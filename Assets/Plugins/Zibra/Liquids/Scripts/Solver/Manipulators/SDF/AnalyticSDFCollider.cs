using System;
using UnityEngine;
using com.zibra.liquid.Solver;
using com.zibra.liquid.Manipulators;
using UnityEngine.Serialization;

namespace com.zibra.liquid.SDFObjects
{
    /// @cond SHOW_DEPRECATED

    /// @deprecated
    /// Only used for backwards compatibility
    [ExecuteInEditMode]
    [Obsolete]
    public class AnalyticSDFCollider : ZibraLiquidCollider
    {
        [SerializeField]
        [FormerlySerializedAs("chosenSDFType")]
        private AnalyticSDF.SDFType ChosenSDFType = AnalyticSDF.SDFType.Sphere;

        [SerializeField]
        private bool InvertSDF = false;

#if UNITY_EDITOR
        public void Awake()
        {
            ZibraLiquidCollider collider = gameObject.AddComponent<ZibraLiquidCollider>();
            if (gameObject.GetComponent<SDFObject>() == null)
            {
                AnalyticSDF sdf = gameObject.AddComponent<AnalyticSDF>();
                sdf.ChosenSDFType = ChosenSDFType;
                sdf.InvertSDF = InvertSDF;
            }

            collider.Friction = Friction;
#if ZIBRA_LIQUID_PAID_VERSION
            collider.ForceInteraction = ForceInteraction;
#endif

            ZibraLiquid[] allLiquids = FindObjectsOfType<ZibraLiquid>();

            foreach (var liquid in allLiquids)
            {
                ZibraLiquidCollider oldCollider = liquid.HasGivenCollider(gameObject);
                if (oldCollider != null)
                {
                    liquid.RemoveCollider(oldCollider);
                    liquid.AddCollider(collider);
                }
            }

            DestroyImmediate(this);
        }
#endif
    }
    /// @endcond
}