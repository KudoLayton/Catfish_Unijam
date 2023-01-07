using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace com.zibra.liquid.Manipulators
{
#if ZIBRA_LIQUID_PAID_VERSION
    /// <summary>
    ///     Data passed to Force Interaction Callback.
    /// </summary>
    public class ForceInteractionData
    {
        /// <summary>
        ///     Force that liquid is about to apply to the object.
        /// </summary>
        public Vector3 Force;
        /// <summary>
        ///     Torque that liquid is about to apply to the object.
        /// </summary>
        public Vector3 Torque;
    }

    /// <summary>
    ///     Type for Force Interaction Callback.
    /// </summary>
    /// <remarks>
    ///     Needs to be separate type for compatibility with older Unity versions.
    /// </remarks>
    [System.Serializable]
    public class ForceInteractionCallbackType : UnityEvent<ForceInteractionData>
    {
    };
#endif

    /// <summary>
    ///     Colliders for the liquid.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Must have SDF to define its shape.
    ///     </para>
    ///     <para>
    ///         You can't use Unity's colliders with the liquid,
    ///         you can only use this specific collider component with liquid.
    ///     </para>
    ///     <para>
    ///         Each collider needs to be added to list of collider in liquid.
    ///         Otherwise it won't do anything.
    ///         This is needed since you may have multiple liquids with separate colliders.
    ///     </para>
    /// </remarks>
    [AddComponentMenu("Zibra/Zibra Liquid Collider")]
    [DisallowMultipleComponent]
    public class ZibraLiquidCollider : Manipulator
    {
#region Public Interface
        /// <summary>
        ///     List of all enabled colliders.
        /// </summary>
        public static readonly List<ZibraLiquidCollider> AllColliders = new List<ZibraLiquidCollider>();

        /// <summary>
        ///     Collider friction.
        /// </summary>
        [FormerlySerializedAs("FluidFriction")]
        [Range(0.0f, 1.0f)]
        public float Friction = 0.0f;

#if ZIBRA_LIQUID_PAID_VERSION
        /// <summary>
        ///     (Unavailable in Free version) Enables Force Interaction feature, allowing liquid to apply force to the
        ///     object.
        /// </summary>
        [Tooltip("Enables Force Interaction feature, allowing liquid to apply force to the object")]
        public bool ForceInteraction;

        /// <summary>
        ///     (Unavailable in Free version) Optional force interaction callback that receives forces applied by
        ///     liquid.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called even if ForceInteraction is disabled, but forces won't be applied by liquid in that case.
        ///     </para>
        ///     <para>
        ///         Can optionally modify forces liquid applies to the object.
        ///     </para>
        /// </remarks>
        [Tooltip("Optional force interaction callback that receives forces applied by liquid")]
        public ForceInteractionCallbackType ForceInteractionCallback;
#endif

        public override ManipulatorType GetManipulatorType()
        {
#if ZIBRA_LIQUID_PAID_VERSION
            if (GetComponent<SDFObjects.NeuralSDF>() != null)
                return ManipulatorType.NeuralCollider;
#if ZIBRA_LIQUID_PRO_VERSION
            else if (GetComponent<SDFObjects.SkinnedMeshSDF>() != null)
                return ManipulatorType.GroupCollider;
#endif
            else
#endif
                return ManipulatorType.AnalyticCollider;
        }

#if UNITY_EDITOR
        public override Color GetGizmosColor()
        {
            switch (GetManipulatorType())
            {
#if ZIBRA_LIQUID_PAID_VERSION
            case ManipulatorType.NeuralCollider:
                return Color.grey;
#endif
#if ZIBRA_LIQUID_PRO_VERSION
            case ManipulatorType.GroupCollider:
                return new Color(0.2f, 0.7f, 0.2f);
#endif
            case ManipulatorType.AnalyticCollider:
            default:
                return new Color(0.2f, 0.9f, 0.9f);
            }
        }
#endif
#endregion
#region Implementation details
        internal void ApplyForceTorque(Vector3 Force, Vector3 Torque)
        {
#if ZIBRA_LIQUID_PAID_VERSION
            ForceInteractionData forceInteractionData = new ForceInteractionData();
            forceInteractionData.Force = Force;
            forceInteractionData.Torque = Torque;

            if (ForceInteractionCallback != null)
            {
                ForceInteractionCallback.Invoke(forceInteractionData);
            }

            if (ForceInteraction)
            {
                Rigidbody rg = GetComponent<Rigidbody>();
                if (rg != null)
                {
                    rg.AddForce(forceInteractionData.Force, ForceMode.Force);
                    rg.AddTorque(forceInteractionData.Torque, ForceMode.Force);
                }
                else
                {
                    Debug.LogWarning(
                        "No rigid body component attached to collider, please add one for force interaction to work");
                }
            }
#endif
        }

        private void Update()
        {
            AdditionalData0.w = Friction;
        }

        private void OnEnable()
        {
            if (!AllColliders?.Contains(this) ?? false)
            {
                AllColliders.Add(this);
            }
        }

        private void OnDisable()
        {
            if (AllColliders?.Contains(this) ?? false)
            {
                AllColliders.Remove(this);
            }
        }
#endregion
    }
}
