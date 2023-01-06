#if ZIBRA_LIQUID_PAID_VERSION
using UnityEngine;
using UnityEditor;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Manipulators;

namespace com.zibra.liquid.Editor.Solver
{
    [CustomEditor(typeof(ZibraLiquidDetector))]
    [CanEditMultipleObjects]
    internal class ZibraLiquidDetectorEditor : ZibraLiquidManipulatorEditor
    {
        private ZibraLiquidDetector[] DetectorInstances;
        private Font MonospaceFont;

        public override void OnInspectorGUI()
        {
            bool missingSDF = false;
#if ZIBRA_LIQUID_PAID_VERSION && !ZIBRA_LIQUID_PRO_VERSION
            bool hasNeuralSDF = false;
#endif

            foreach (var instance in DetectorInstances)
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
                if (DetectorInstances.Length > 1)
                    EditorGUILayout.HelpBox("At least 1 detector missing shape. Please add Zibra SDF.",
                                            MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Missing detector shape. Please add Zibra SDF.", MessageType.Error);
                if (GUILayout.Button(DetectorInstances.Length > 1 ? "Add Analytic SDFs" : "Add Analytic SDF"))
                {
                    foreach (var instance in DetectorInstances)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<AnalyticSDF>(instance.gameObject);
                        }
                    }
                }
#if ZIBRA_LIQUID_PRO_VERSION
                if (GUILayout.Button(DetectorInstances.Length > 1 ? "Add Neural SDFs" : "Add Neural SDF"))
                {
                    foreach (var instance in DetectorInstances)
                    {
                        if (instance.GetComponent<SDFObject>() == null)
                        {
                            Undo.AddComponent<NeuralSDF>(instance.gameObject);
                        }
                    }
                }
                if (GUILayout.Button(DetectorInstances.Length > 1 ? "Add Skinned Mesh SDFs" : "Add Skinned Mesh SDF"))
                {
                    foreach (var instance in DetectorInstances)
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
                if (DetectorInstances.Length > 1)
                    EditorGUILayout.HelpBox(
                        "At least 1 detector has Neural SDF. Neural SDFs on detectors are not supported in this version.",
                        MessageType.Error);
                else
                    EditorGUILayout.HelpBox("Neural SDFs on detectors are not supported in this version",
                                            MessageType.Error);
                if (GUILayout.Button(DetectorInstances.Length > 1 ? "Replace Neural SDFs with Analytic SDFs"
                                                                  : "Replace Neural SDF with Analytic SDF"))
                {
                    foreach (var instance in DetectorInstances)
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

            if (DetectorInstances.Length > 1)
                GUILayout.Label("Multiple detectors selected. Showing sum of all selected instances. Not showing bounding boxes.");
            int particlesInside = 0;
            foreach (var instance in DetectorInstances)
            {
                particlesInside += instance.ParticlesInside;
            }

            Font defaultFont = GUI.skin.font;
            GUI.skin.font = MonospaceFont;

            GUILayout.Label("Amount of particles inside the detector: " + particlesInside);
            
            if (DetectorInstances.Length == 1)
            {
                GUILayout.Label("Bounding box min: " + DetectorInstances[0].BoundingBoxMin);
                GUILayout.Label("Bounding box max: " + DetectorInstances[0].BoundingBoxMax);
            }

            GUI.skin.font = defaultFont;

#if ZIBRA_LIQUID_PRO_VERSION
            EditorGUILayout.PropertyField(CurrentInteractionMode);
            EditorGUILayout.PropertyField(ParticleSpecies);
            serializedObject.ApplyModifiedProperties();
#endif
        }

        // clang-format doesn't parse code with new keyword properly
        // clang-format off

        protected new void OnEnable()
        {
            base.OnEnable();

            MonospaceFont = EditorGUIUtility.Load("Fonts/RobotoMono/RobotoMono-Regular.ttf") as Font;

            DetectorInstances = new ZibraLiquidDetector[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                DetectorInstances[i] = targets[i] as ZibraLiquidDetector;
            }
        }
    }
}
#endif