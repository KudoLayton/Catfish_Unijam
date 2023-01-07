#if ZIBRA_LIQUID_PAID_VERSION
using UnityEngine;
using UnityEditor;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Manipulators;

namespace com.zibra.liquid.Editor.Solver
{
    [CustomEditor(typeof(ZibraLiquidVoid))]
    [CanEditMultipleObjects]
    internal class ZibraLiquidVoidEditor : ZibraLiquidManipulatorEditor
    {
        private ZibraLiquidVoid[] VoidInstances;

        public override void OnInspectorGUI()
        {
            bool missingSDF = false;
#if ZIBRA_LIQUID_PAID_VERSION && !ZIBRA_LIQUID_PRO_VERSION
            bool hasNeuralSDF = false;
#endif

            foreach (var instance in VoidInstances)
            {
                SDFObject sdf = instance.GetComponent<SDFObject>();
                if (sdf == null)
                {
                    missingSDF = true;
                    continue;
                }

#if ZIBRA_LIQUID_PAID_VERSION && !ZIBRA_LIQUID_PRO_VERSION
                if (sdf is NeuralSDF)
                {
                    hasNeuralSDF = true;
                    continue;
                }
#endif
            }

            if (missingSDF)
            {
                if (VoidInstances.Length > 1)
                    EditorGUILayout.HelpBox("At least 1 void missing shape. Please add Zibra SDF.", MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Missing void shape. Please add Zibra SDF.", MessageType.Error);
                if (GUILayout.Button(VoidInstances.Length > 1 ? "Add Analytic SDFs" : "Add Analytic SDF"))
                {
                    foreach (var instance in VoidInstances)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<AnalyticSDF>(instance.gameObject);
                        }
                    }
                }
#if ZIBRA_LIQUID_PRO_VERSION
                if (GUILayout.Button(VoidInstances.Length > 1 ? "Add Neural SDFs" : "Add Neural SDF"))
                {
                    foreach (var instance in VoidInstances)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<NeuralSDF>(instance.gameObject);
                        }
                    }
                }
                if (GUILayout.Button(VoidInstances.Length > 1 ? "Add Skinned Mesh SDFs" : "Add Skinned Mesh SDF"))
                {
                    foreach (var instance in VoidInstances)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<SkinnedMeshSDF>(instance.gameObject);
                        }
                    }
                }
#endif
                GUILayout.Space(5);
            }

#if ZIBRA_LIQUID_PAID_VERSION && !ZIBRA_LIQUID_PRO_VERSION
            if (hasNeuralSDF)
            {
                if (VoidInstances.Length > 1)
                    EditorGUILayout.HelpBox(
                        "At least 1 void has Neural SDF. Neural SDFs on voids are not supported in this version.",
                        MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Neural SDFs on voids are not supported in this version",
                                            MessageType.Error);
                if (GUILayout.Button(VoidInstances.Length > 1 ? "Replace Neural SDFs with Analytic SDFs"
                                                              : "Replace Neural SDF with Analytic SDF"))
                {
                    foreach (var instance in VoidInstances)
                    {
                        var sdf = instance.GetComponent<NeuralSDF>();
                        if (sdf != null)
                        {
                            Undo.RecordObject(instance.gameObject, "Added Analytic SDF");
                            DestroyImmediate(sdf);
                            Undo.AddComponent<AnalyticSDF>(instance.gameObject);
                        }
                    }
                }
                GUILayout.Space(5);
            }
#endif

            if (VoidInstances.Length > 1)
                GUILayout.Label("Multiple voids selected. Showing sum of all selected instances.");
            long deletedTotal = 0;
            int deletedCurrentFrame = 0;
            foreach (var instance in VoidInstances)
            {
                deletedTotal += instance.DeletedParticleCountTotal;
                deletedCurrentFrame += instance.DeletedParticleCountPerFrame;
            }
            GUILayout.Label("Total amount of deleted particles: " + deletedTotal);
            GUILayout.Label("Deleted particles per frame: " + deletedCurrentFrame);

#if ZIBRA_LIQUID_PRO_VERSION
            EditorGUILayout.PropertyField(CurrentInteractionMode);
            EditorGUILayout.PropertyField(ParticleSpecies);
#endif
            serializedObject.ApplyModifiedProperties();
        }

        // clang-format doesn't parse code with new keyword properly
        // clang-format off

        protected new void OnEnable()
        {
            base.OnEnable();

            VoidInstances = new ZibraLiquidVoid[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                VoidInstances[i] = targets[i] as ZibraLiquidVoid;
            }
        }
    }
}
#endif