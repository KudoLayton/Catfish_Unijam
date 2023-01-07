#if ZIBRA_LIQUID_PAID_VERSION && UNITY_EDITOR

using com.zibra.liquid.DataStructures;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Utilities;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace com.zibra.liquid.Editor.SDFObjects
{
    /// <summary>
    ///     (Editor only, Unavailable in Free version) Static class, responsible for scheduling generation requests.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This class is a queue of objects to generate.
    ///         If it's not empty, we are generating exactly 1 object.
    ///         As soon as generation of an object ends, either next one starts,
    ///         or queue becomes empty and generation stops.
    ///     </para>
    ///     <para>
    ///         Note that generation uses ZibraAI's servers,
    ///         and so:
    ///         1. It sends your mesh to that server
    ///         2. If generating too much you may get rate limited and generation may fail
    ///     </para>
    ///     <para>
    ///         All requests processed in FIFO order: First In - First Out.
    ///     </para>
    /// </remarks>
    public static class GenerationQueue
    {
#region Public Interface
        /// <summary>
        ///     Adds <see cref="SDFObjects::NeuralSDF">NeuralSDF</see> to the generation queue.
        /// </summary>
        /// <remarks>
        ///     All objects in the generation queue will be generated automatically.
        /// </remarks>
        public static void AddToQueue(NeuralSDF sdf)
        {
            if (Contains(sdf))
                return;

            Mesh objectMesh = MeshUtilities.GetMesh(sdf.gameObject);
            if (objectMesh == null)
                return;

            MeshNeuralSDFGenerator gen =
                new MeshNeuralSDFGenerator(sdf.ObjectRepresentation, objectMesh, sdf.gameObject);
            AddToQueue(gen);
            Generators[gen] = sdf;
        }

#if ZIBRA_LIQUID_PRO_VERSION
        /// <summary>
        ///     (Pro version only) Adds <see cref="SDFObjects::SkinnedMeshSDF">SkinnedMeshSDF</see> to the generation
        ///     queue.
        /// </summary>
        /// <remarks>
        ///     All objects in the generation queue will be generated automatically.
        /// </remarks>
        public static void AddToQueue(SkinnedMeshSDF sdf)
        {
            if (Contains(sdf))
                return;

            sdf.BoneSDFList.Clear();

            SkinnedMeshRenderer instanceSkinnedMeshRenderer = sdf.GetComponent<SkinnedMeshRenderer>();

            if (instanceSkinnedMeshRenderer == null)
                return;

            Transform[] bones = instanceSkinnedMeshRenderer.bones;

            List<Mesh> boneMeshes = MeshUtilities.GetSkinnedMeshBoneMeshes(sdf.gameObject);

            for (int i = 0; i < bones.Length; i++)
            {
                Transform bone = bones[i];

                GameObject boneObject;

                Transform bonetransform = bone.Find("BoneNeuralSDF");
                if (bonetransform != null)
                {
                    GameObject.DestroyImmediate(bonetransform.gameObject);
                }

                if (boneMeshes[i].vertexCount == 0)
                {
                    continue;
                }

                boneObject = new GameObject();
                boneObject.name = "BoneNeuralSDF";
                boneObject.transform.SetParent(sdf.transform, false);
                boneObject.AddComponent<NeuralSDF>();
                NeuralSDF boneSDF = boneObject.GetComponent<NeuralSDF>();
                sdf.BoneSDFList.Add(boneSDF);
            }

            SkinnedNeuralSDFGenerator gen =
                new SkinnedNeuralSDFGenerator(sdf.BoneSDFList, bones, instanceSkinnedMeshRenderer, sdf.gameObject);
            AddToQueue(gen);
            SkinnedGenerators[gen] = sdf;
        }
#endif

        /// <summary>
        ///     Clears generation queue, canceling all generation requests.
        /// </summary>
        public static void Abort()
        {
            if (SDFsToGenerate.Count > 0)
            {
                SDFsToGenerate.Peek().Abort();
                SDFsToGenerate.Clear();
                Generators.Clear();
#if ZIBRA_LIQUID_PRO_VERSION
                SkinnedGenerators.Clear();
#endif
                EditorApplication.update -= Update;
            }
        }

        /// <summary>
        ///     Returns number of elements in the queue.
        /// </summary>
        public static int GetQueueLength()
        {
            return SDFsToGenerate.Count;
        }

        /// <summary>
        ///     Checks if <see cref="SDFObjects::NeuralSDF">NeuralSDF</see> is queued for generation.
        /// </summary>
        public static bool Contains(NeuralSDF sdf)
        {
            return Generators.ContainsValue(sdf);
        }

#if ZIBRA_LIQUID_PRO_VERSION

        /// <summary>
        ///     (Pro version only) Checks if <see cref="SDFObjects::SkinnedMeshSDF">SkinnedMeshSDF</see>
        ///     is queued for generation.
        /// </summary>
        public static bool Contains(SkinnedMeshSDF sdf)
        {
            return SkinnedGenerators.ContainsValue(sdf);
        }
#endif
#endregion
#region Implementation details
        private static Queue<NeuralSDFGenerator> SDFsToGenerate = new Queue<NeuralSDFGenerator>();
        private static Dictionary<MeshNeuralSDFGenerator, NeuralSDF> Generators =
            new Dictionary<MeshNeuralSDFGenerator, NeuralSDF>();
#if ZIBRA_LIQUID_PRO_VERSION
        private static Dictionary<SkinnedNeuralSDFGenerator, SkinnedMeshSDF> SkinnedGenerators =
            new Dictionary<SkinnedNeuralSDFGenerator, SkinnedMeshSDF>();
#endif

        private static void Update()
        {
            if (SDFsToGenerate.Count == 0)
                Abort();

            SDFsToGenerate.Peek().Update();
            if (SDFsToGenerate.Peek().IsFinished())
            {
                RemoveFromQueue();
                if (SDFsToGenerate.Count > 0)
                {
                    SDFsToGenerate.Peek().Start();
                }
            }
        }

        private static void RemoveFromQueue()
        {
            if (SDFsToGenerate.Peek() is MeshNeuralSDFGenerator)
                Generators.Remove(SDFsToGenerate.Peek() as MeshNeuralSDFGenerator);

#if ZIBRA_LIQUID_PRO_VERSION
            if (SDFsToGenerate.Peek() is SkinnedNeuralSDFGenerator)
                SkinnedGenerators.Remove(SDFsToGenerate.Peek() as SkinnedNeuralSDFGenerator);
#endif

            SDFsToGenerate.Dequeue();

            if (SDFsToGenerate.Count == 0)
            {
                EditorApplication.update -= Update;
            }
        }

        private static void AddToQueue(NeuralSDFGenerator generator)
        {
            if (!SDFsToGenerate.Contains(generator))
            {
                if (SDFsToGenerate.Count == 0)
                {
                    EditorApplication.update += Update;
                    generator.Start();
                }
                SDFsToGenerate.Enqueue(generator);
            }
        }
#endregion
    }

    internal abstract class NeuralSDFGenerator
    {
        // Limits for representation generation web requests
        protected const uint REQUEST_TRIANGLE_COUNT_LIMIT = 100000;
        protected const uint REQUEST_SIZE_LIMIT = 3 << 20; // 3mb

        protected Mesh MeshToProcess;
        protected Bounds MeshBounds;
        protected UnityWebRequest CurrentRequest;
        protected GameObject GameObjectToMarkDirty;

        public abstract void Start();

        protected bool CheckMeshSize()
        {
            if (MeshToProcess.triangles.Length / 3 > REQUEST_TRIANGLE_COUNT_LIMIT)
            {
                string errorMessage =
                    $"Mesh is too large. Can't generate representation. Triangle count should not exceed {REQUEST_TRIANGLE_COUNT_LIMIT} triangles, but current mesh have {MeshToProcess.triangles.Length / 3} triangles";
                EditorUtility.DisplayDialog("ZibraLiquid Error.", errorMessage, "OK");
                Debug.LogError(errorMessage);
                return true;
            }
            return false;
        }

        protected void SendRequest(string requestURL, string json)
        {
            if (CurrentRequest != null)
            {
                CurrentRequest.Dispose();
                CurrentRequest = null;
            }

            if (json.Length > REQUEST_SIZE_LIMIT)
            {
                string errorMessage =
                    $"Mesh is too large. Can't generate representation. Please decrease vertex/triangle count. Web request should not exceed {REQUEST_SIZE_LIMIT / (1 << 20):N2}mb, but for current mesh {(float)json.Length / (1 << 20):N2}mb is needed.";
                EditorUtility.DisplayDialog("ZibraLiquid Error.", errorMessage, "OK");
                Debug.LogError(errorMessage);
                return;
            }

            if (ZibraServerAuthenticationManager.GetInstance().GetStatus() ==
                ZibraServerAuthenticationManager.Status.OK)
            {
                if (requestURL != "")
                {
                    CurrentRequest = UnityWebRequest.Post(requestURL, json);
                    CurrentRequest.SendWebRequest();
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Zibra Liquid Error",
                                            ZibraServerAuthenticationManager.GetInstance().GetErrorMessage(), "Ok");
                Debug.LogError(ZibraServerAuthenticationManager.GetInstance().GetErrorMessage());
            }
        }

        public void Abort()
        {
            CurrentRequest?.Dispose();
        }

        protected abstract void ProcessResult();

        public void Update()
        {
            if (CurrentRequest != null && CurrentRequest.isDone)
            {
#if UNITY_2020_2_OR_NEWER
                if (CurrentRequest.isDone && CurrentRequest.result == UnityWebRequest.Result.Success)
#else
                if (CurrentRequest.isDone && !CurrentRequest.isHttpError && !CurrentRequest.isNetworkError)
#endif
                {
                    ProcessResult();
                }
                else
                {
                    EditorUtility.DisplayDialog("Zibra Liquid Server Error", CurrentRequest.error, "Ok");
                    Debug.LogError(CurrentRequest.downloadHandler.text);
                }

                CurrentRequest.Dispose();
                CurrentRequest = null;

                // make sure to mark the scene as changed
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                    UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
            }
        }

        public bool IsFinished()
        {
            return CurrentRequest == null;
        }
    }

    internal class MeshNeuralSDFGenerator : NeuralSDFGenerator
    {
        private NeuralSDFRepresentation NeuralSDFInstance;

        public MeshNeuralSDFGenerator(NeuralSDFRepresentation NeuralSDF, Mesh mesh, GameObject gameObject)
        {
            MeshToProcess = mesh;
            NeuralSDFInstance = NeuralSDF;
            GameObjectToMarkDirty = gameObject;
        }

        public NeuralSDFRepresentation GetSDF()
        {
            return NeuralSDFInstance;
        }

        public void CreateMeshBBCube()
        {
            MeshBounds = MeshToProcess.bounds;
            NeuralSDFInstance.BoundingBoxCenter = MeshBounds.center;
            NeuralSDFInstance.BoundingBoxSize = MeshBounds.size;
        }

        public override void Start()
        {
            if (CheckMeshSize())
                return;

            var meshRepresentation =
                new MeshRepresentation { vertices = MeshToProcess.vertices.Vector3ToString(),
                                         faces = MeshToProcess.triangles.IntToString(),
                                         vox_dim = NeuralSDFRepresentation.DEFAULT_EMBEDDING_GRID_DIMENSION,
                                         sdf_dim = NeuralSDFRepresentation.DEFAULT_SDF_APPROX_DIMENSION,
                                         cutoff_weight = 0.1f,
                                         static_quantization = true };

            var json = JsonUtility.ToJson(meshRepresentation);
            string requestURL = ZibraServerAuthenticationManager.GetInstance().GenerationURL;

            SendRequest(requestURL, json);
        }

        protected override void ProcessResult()
        {
            var json = CurrentRequest.downloadHandler.text;
            VoxelRepresentation newRepresentation =
                JsonUtility.FromJson<SkinnedVoxelRepresentation>(json).meshes_data[0];

            if (string.IsNullOrEmpty(newRepresentation.embeds) || string.IsNullOrEmpty(newRepresentation.sd_grid))
            {
                EditorUtility.DisplayDialog("Zibra Liquid Server Error",
                                            "Server returned empty result. Contact Zibra Liquid support", "Ok");
                Debug.LogError("Server returned empty result. Contact Zibra Liquid support");

                return;
            }

            CreateMeshBBCube();

            NeuralSDFInstance.CurrentRepresentationV3 = newRepresentation;
            NeuralSDFInstance.CreateRepresentation(NeuralSDFRepresentation.DEFAULT_EMBEDDING_GRID_DIMENSION,
                                                   NeuralSDFRepresentation.DEFAULT_SDF_APPROX_DIMENSION);

            EditorUtility.SetDirty(GameObjectToMarkDirty);
        }
    }

#if ZIBRA_LIQUID_PRO_VERSION
    internal class SkinnedNeuralSDFGenerator : NeuralSDFGenerator
    {
        private List<SDFObject> NeuralSDFInstances;
        private Transform[] BoneTransforms;
        private SkinnedMeshRenderer renderer;

        public SkinnedNeuralSDFGenerator(List<SDFObject> NeuralSDFs, Transform[] bones, SkinnedMeshRenderer r,
                                         GameObject gameObject)
        {
            renderer = r;
            MeshToProcess = MeshUtilities.GetMesh(r.gameObject);
            NeuralSDFInstances = NeuralSDFs;
            BoneTransforms = bones;
            GameObjectToMarkDirty = gameObject;
        }

        public override void Start()
        {
            if (CheckMeshSize())
                return;

            int[] bone_ids = new int[MeshToProcess.vertexCount * 4];
            float[] bone_weights = new float[MeshToProcess.vertexCount * 4];

            Mesh sharedMesh = renderer.sharedMesh;

            for (int i = 0; i < sharedMesh.vertexCount; i++)
            {
                var weight = sharedMesh.boneWeights[i];
                bone_ids[i * 4 + 0] = weight.boneIndex0;
                bone_ids[i * 4 + 1] = (weight.weight1 == 0.0f) ? -1 : weight.boneIndex1;
                bone_ids[i * 4 + 2] = (weight.weight2 == 0.0f) ? -1 : weight.boneIndex2;
                bone_ids[i * 4 + 3] = (weight.weight3 == 0.0f) ? -1 : weight.boneIndex3;

                bone_weights[i * 4 + 0] = weight.weight0;
                bone_weights[i * 4 + 1] = weight.weight1;
                bone_weights[i * 4 + 2] = weight.weight2;
                bone_weights[i * 4 + 3] = weight.weight3;
            }

            var meshRepresentation =
                new SkinnedMeshRepresentation { vertices = MeshToProcess.vertices.Vector3ToString(),
                                                faces = MeshToProcess.triangles.IntToString(),
                                                bone_ids = bone_ids.IntToString(),
                                                bone_weights = bone_weights.FloatToString(),
                                                vox_dim = NeuralSDFRepresentation.DEFAULT_EMBEDDING_GRID_DIMENSION,
                                                sdf_dim = NeuralSDFRepresentation.DEFAULT_SDF_APPROX_DIMENSION,
                                                cutoff_weight = 0.1f,
                                                static_quantization = true };

            var json = JsonUtility.ToJson(meshRepresentation);
            string requestURL = ZibraServerAuthenticationManager.GetInstance().GenerationURL;

            SendRequest(requestURL, json);
        }

        protected override void ProcessResult()
        {
            SkinnedVoxelRepresentation newRepresentation = null;

            var json = CurrentRequest.downloadHandler.text;
            newRepresentation = JsonUtility.FromJson<SkinnedVoxelRepresentation>(json);

            if (newRepresentation.meshes_data == null)
            {
                EditorUtility.DisplayDialog("Zibra Liquid Server Error",
                                            "Server returned empty result. Contact Zibra Liquid support", "Ok");
                Debug.LogError("Server returned empty result. Contact Zibra Liquid support");

                return;
            }

            for (int i = 0; i < newRepresentation.meshes_data.Length; i++)
            {
                var representation = newRepresentation.meshes_data[i];

                if (string.IsNullOrEmpty(representation.embeds) || string.IsNullOrEmpty(representation.sd_grid))
                {
                    continue;
                }

                var instance = NeuralSDFInstances[i];

                if (instance is NeuralSDF neuralSDF)
                {
                    neuralSDF.ObjectRepresentation.CurrentRepresentationV3 = representation;
                    neuralSDF.ObjectRepresentation.CreateRepresentation(
                        NeuralSDFRepresentation.DEFAULT_EMBEDDING_GRID_DIMENSION,
                        NeuralSDFRepresentation.DEFAULT_SDF_APPROX_DIMENSION);
                    neuralSDF.transform.SetParent(BoneTransforms[i], true);
                }
            }

            EditorUtility.SetDirty(GameObjectToMarkDirty);
        }
    }
#endif
}

#endif