using UnityEngine;
using UnityEditor;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Manipulators;

namespace com.zibra.liquid.Editor.SDFObjects
{
    [CustomEditor(typeof(ZibraLiquidCollider))]
    [CanEditMultipleObjects]
    internal class ColliderEditor : UnityEditor.Editor
    {
        private static ColliderEditor EditorInstance;

        private ZibraLiquidCollider[] Colliders;

        private SerializedProperty Friction;
        private SerializedProperty ForceInteraction;
        private SerializedProperty ForceInteractionCallback;

        private void Awake()
        {
#if ZIBRA_LIQUID_PAID_VERSION
            ZibraServerAuthenticationManager.GetInstance();
#endif
        }

        private void OnEnable()
        {
            EditorInstance = this;

            Colliders = new ZibraLiquidCollider[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                Colliders[i] = targets[i] as ZibraLiquidCollider;
            }

            serializedObject.Update();
            Friction = serializedObject.FindProperty("Friction");
            ForceInteraction = serializedObject.FindProperty("ForceInteraction");
            ForceInteractionCallback = serializedObject.FindProperty("ForceInteractionCallback");
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDisable()
        {
            if (EditorInstance == this)
            {
                EditorInstance = null;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

#if ZIBRA_LIQUID_PAID_VERSION
            bool isRigidbodyComponentMissing = false;
#endif
            bool missingSDF = false;

            foreach (var instance in Colliders)
            {
#if ZIBRA_LIQUID_PAID_VERSION
                if (instance.ForceInteraction && instance.GetComponent<Rigidbody>() == null)
                {
                    isRigidbodyComponentMissing = true;
                }
#endif
                SDFObject sdf = instance.GetComponent<SDFObject>();
                if (sdf == null)
                {
                    missingSDF = true;
                }
            }

            if (missingSDF)
            {
                if (Colliders.Length > 1)
                    EditorGUILayout.HelpBox("At least 1 collider missing shape. Please add Zibra SDF.",
                                            MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Missing collider shape. Please add Zibra SDF.", MessageType.Error);
                if (GUILayout.Button(Colliders.Length > 1 ? "Add Analytic SDFs" : "Add Analytic SDF"))
                {
                    foreach (var instance in Colliders)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<AnalyticSDF>(instance.gameObject);
                        }
                    }
                }
#if ZIBRA_LIQUID_PAID_VERSION
                if (GUILayout.Button(Colliders.Length > 1 ? "Add Neural SDFs" : "Add Neural SDF"))
                {
                    foreach (var instance in Colliders)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<NeuralSDF>(instance.gameObject);
                        }
                    }
                }
#endif

#if ZIBRA_LIQUID_PRO_VERSION
                if (GUILayout.Button(Colliders.Length > 1 ? "Add Skinned Mesh SDFs" : "Add Skinned Mesh SDF"))
                {
                    foreach (var instance in Colliders)
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

#if ZIBRA_LIQUID_PAID_VERSION
            if (isRigidbodyComponentMissing)
            {
                if (Colliders.Length > 1)
                    EditorGUILayout.HelpBox(
                        "At least 1 collider has force interaction enabled, but doesn't have rigidbody to apply force to.",
                        MessageType.Error);
                else
                    EditorGUILayout.HelpBox(
                        "Collider has force interaction enabled, but doesn't have rigidbody to apply force to.",
                        MessageType.Error);

                if (GUILayout.Button(Colliders.Length > 1 ? "Add Rigidbodies" : "Add Rigidbody"))
                {
                    foreach (var instance in Colliders)
                    {
                        if (instance.ForceInteraction && instance.GetComponent<Rigidbody>() == null)
                        {
                            Undo.AddComponent<Rigidbody>(instance.gameObject);
                        }
                    }
                }

                if (GUILayout.Button("Disable force interaction"))
                {
                    foreach (var instance in Colliders)
                    {
                        if (instance.ForceInteraction && instance.GetComponent<Rigidbody>() == null)
                        {
                            instance.ForceInteraction = false;
                        }
                    }
                }

                GUILayout.Space(5);
            }

            EditorGUILayout.PropertyField(Friction);
            EditorGUILayout.PropertyField(ForceInteraction);
            EditorGUILayout.PropertyField(ForceInteractionCallback);
#endif

            serializedObject.ApplyModifiedProperties();
        }
    }
}
