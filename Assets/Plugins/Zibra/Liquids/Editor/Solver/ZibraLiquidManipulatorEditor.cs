using com.zibra.liquid.Manipulators;
using UnityEditor;

namespace com.zibra.liquid.Editor.Solver
{
    internal class ZibraLiquidManipulatorEditor : UnityEditor.Editor
    {
#if ZIBRA_LIQUID_PRO_VERSION
        protected SerializedProperty CurrentInteractionMode;
        protected SerializedProperty ParticleSpecies;
#endif

        protected void TriggerRepaint()
        {
            Repaint();
        }

        protected void OnEnable()
        {
            Manipulator manipulator = target as Manipulator;
            manipulator.OnChanged += TriggerRepaint;

#if ZIBRA_LIQUID_PRO_VERSION
            ParticleSpecies = serializedObject.FindProperty("ParticleSpecies");
            CurrentInteractionMode = serializedObject.FindProperty("CurrentInteractionMode");
#endif
        }

        protected void OnDisable()
        {
            Manipulator manipulator = target as Manipulator;
            manipulator.OnChanged -= TriggerRepaint;
        }
    }
}
