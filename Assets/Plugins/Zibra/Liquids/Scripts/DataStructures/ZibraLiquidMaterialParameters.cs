using System;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
using UnityEditor;
#endif

namespace com.zibra.liquid.DataStructures
{
    /// <summary>
    ///     Component that contains liquid material parameters.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         It doesn't execute anything by itself, it is used by <see cref="ZibraLiquid"/> instead.
    ///     </para>
    ///     <para>
    ///         It's separated so you can save and apply presets for this component separately.
    ///     </para>
    /// </remarks>
    [ExecuteInEditMode]
    public class ZibraLiquidMaterialParameters : MonoBehaviour
    {
#region Public Interface
#if ZIBRA_LIQUID_PRO_VERSION
        /// <summary>
        ///     (Pro version only) Container for parameters of additional liquid materials.
        /// </summary>
        /// <remarks>
        ///     Few material parameters are global for whole liquid,
        ///     and can only be adjusted from main material parameters.
        /// </remarks>
        [System.Serializable]
        public class LiquidMaterial
        {
            /// <summary>
            ///     Color of the liquid.
            /// </summary>
            /// <remarks>
            ///     Opacity can be adjusted via <see cref="ScatteringAmount"/> and <see cref="AbsorptionAmount"/>.
            /// </remarks>
            [Tooltip("Color of the liquid")]
            public Color Color = new Color(0.3411765f, 0.92156863f, 0.85236126f, 1.0f);

            /// <summary>
            ///     Emissive color of the liquid.
            /// </summary>
            [ColorUsage(true, true)]
            [Tooltip("Emissive color of the liquid")]
            public Color EmissiveColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

            /// <summary>
            ///     Amount of light scattering happening inside the liquid.
            /// </summary>
            /// <remarks>
            ///     Affects opacity.
            /// </remarks>
            [Tooltip("Amount of light scattering happening inside the liquid. Affects opacity.")]
            [Range(0.0f, 100.0f)]
            public float ScatteringAmount = 5.0f;

            /// <summary>
            ///     Amount of light absorption happening inside the liquid.
            /// </summary>
            /// <remarks>
            ///     Affects opacity.
            /// </remarks>
            [Tooltip("Amount of light absorption happening inside the liquid. Affects opacity.")]
            [Range(0.0f, 100.0f)]
            public float AbsorptionAmount = 20.0f;

            /// <summary>
            ///     Roughness of the liquid surface.
            /// </summary>
            [Tooltip("Roughness of the liquid surface")]
            [Range(0.0f, 1.0f)]
            public float Roughness = 0.3f;

            /// <summary>
            ///     Metalness of the liquid surface.
            /// </summary>
            [Tooltip("Metalness of the liquid surface")]
            [Range(0.0f, 1.0f)]
            public float Metalness = 0.3f;
        }
#endif

        /// <summary>
        ///     Material that will be used to render liquid in Mesh Render mode.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If you want to create your own material, you'll need to use default one as a reference.
        ///     </para>
        ///     <para>
        ///         This is the material that gets parameters defined in <see cref="ZibraLiquidMaterialParameters"/>
        ///     </para>
        ///     <para>
        ///         If you set it to null in Editor, it'll revert to default.
        ///     </para>
        /// </remarks>
        [Tooltip("Material that will be used to render liquid in Mesh Render mode")]
        public Material FluidMeshMaterial;

        /// <summary>
        ///     Material that will be used to upscale liquid in Mesh Render mode.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Most users won't need to customize this material,
        ///         but if you want to create your own material, you'll need to use default one as a reference.
        ///     </para>
        ///     <para>
        ///         Has no effect unless you enable downscale in ZibraLiquid component.
        ///     </para>
        ///     <para>
        ///         If you set it to null in Editor, it'll revert to default.
        ///     </para>
        /// </remarks>
        [Tooltip("Material that will be used to upscale liquid in Mesh Render mode")]
        public Material UpscaleMaterial;

        // Don't think anyone will need to edit this material
        // But if anyone will ever need that, removing [HideInInspector] will work

        /// <summary>
        ///     Material that will be used to upscale liquid in Mesh Render mode.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This material is meant to only be used for debugging.
        ///     </para>
        ///     <para>
        ///         We don't expect that anyone will need to modify this,
        ///         but if you want to create your own material, you'll need to use default one as a reference.
        ///     </para>
        ///     <para>
        ///         Has no effect unless you enable VisualizeSceneSDF in ZibraLiquid component.
        ///     </para>
        ///     <para>
        ///         If you set it to null in Editor, it'll revert to default.
        ///     </para>
        /// </remarks>
        [HideInInspector]
        public Material SDFRenderMaterial;

        /// <summary>
        ///     Color of the liquid.
        /// </summary>
        /// <remarks>
        ///     Opacity can be adjusted via <see cref="ScatteringAmount"/> and <see cref="AbsorptionAmount"/>.
        /// </remarks>
        [Tooltip("Color of the liquid")]
        [FormerlySerializedAs("RefractionColor")]
        public Color Color = new Color(0.3411765f, 0.92156863f, 0.85236126f, 1.0f);

        /// <summary>
        ///     Color of the reflections on the liquid surface.
        /// </summary>
        [Tooltip("Color of the reflections on the liquid surface")]
        [ColorUsage(true, true)]
#if UNITY_PIPELINE_HDRP
        public Color ReflectionColor = new Color(0.004434771f, 0.004434771f, 0.004434771f, 1.0f);
#else
        public Color ReflectionColor = new Color(1.39772f, 1.39772f, 1.39772f, 1.0f);
#endif

        /// <summary>
        ///     Emissive color of the liquid.
        /// </summary>
        /// <remarks>
        ///     Normally pure black for most liquids.
        /// </remarks>
        [Tooltip("Emissive color of the liquid. Normally pure black for most liquids.")]
        [ColorUsage(true, true)]
        public Color EmissiveColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        ///     Roughness of the liquid surface.
        /// </summary>
        [Tooltip("Roughness of the liquid surface")]
        [Range(0.0f, 1.0f)]
        public float Roughness = 0.04f;

        /// <summary>
        ///     Metalness of the liquid surface.
        /// </summary>
        [Tooltip("Metalness of the liquid surface")]
        [FormerlySerializedAs("Metal")]
        [Range(0.0f, 1.0f)]
        public float Metalness = 0.3f;

        /// <summary>
        ///     Amount of light scattering happening inside the liquid.
        /// </summary>
        /// <remarks>
        ///     Affects opacity.
        /// </remarks>
        [Tooltip("Amount of light scattering happening inside the liquid. Affects opacity.")]
        [Range(0.0f, 400.0f)]
        public float ScatteringAmount = 5.0f;

        /// <summary>
        ///     Amount of light absorption happening inside the liquid.
        /// </summary>
        /// <remarks>
        ///     Affects opacity.
        /// </remarks>
        [Tooltip("Amount of light absorption happening inside the liquid. Affects opacity.")]
        [FormerlySerializedAs("Opacity")]
        [Range(0.0f, 400.0f)]
        public float AbsorptionAmount = 20.0f;

        /// <summary>
        ///     Index of refraction.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Value of 1.0 corresponds to no refraction, same behaviour as normal transparent materials.
        ///     </para>
        ///     <para>
        ///         Values for common materials for reference: Water - ~1.33, Oil - ~1.47, Glass - ~1.52
        ///     </para>
        /// </remarks>
        [Tooltip("Index of refraction")]
        [Range(1.0f, 3.0f)]
        public float IndexOfRefraction = 1.333f;

        /// <summary>
        ///     Radius of the blur that is applied to liquid density before mesh generation.
        /// </summary>
        /// <remarks>
        ///     Hihger values produce smoother liquid mesh normals.
        /// </remarks>
        [Tooltip("Radius of the blur that is applied to liquid density before mesh generation")]
        [Range(0.01f, 4.0f)]
        public float FluidSurfaceBlur = 1.5f;

#if ZIBRA_LIQUID_PRO_VERSION
        /// <summary>
        ///     (Pro version only) Radius of the blur that is applied to liquid density before mesh generation.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Hihger values produce smoother liquid mesh normals.
        ///     </para>
        ///     <para>
        ///         Foam parameters have no effect outside of Mesh Render mode.
        ///     </para>
        /// </remarks>
        [Tooltip("Radius of the blur that is applied to liquid density before mesh generation")]
        [FormerlySerializedAs("Foam")]
        [Range(0.0f, 3.0f)]
        public float FoamIntensity = 0.8f;

        /// <summary>
        ///     (Pro version only) Rate of foam decay.
        /// </summary>
        /// <remarks>
        ///     Foam parameters have no effect outside of Mesh Render mode.
        /// </remarks>
        [Tooltip("Rate of foam decay")]
        [Range(0.0f, 0.1f)]
        public float FoamDecay = 0.01f;

        /// <summary>
        ///     (Pro version only) Foam spawn threshold.
        /// </summary>
        /// <remarks>
        ///     Foam parameters have no effect outside of Mesh Render mode.
        /// </remarks>
        [Tooltip("Foam spawn threshold")]
        [FormerlySerializedAs("FoamDensity")]
        [Range(0.0f, 1.0f)]
        public float FoamAmount = 1.0f;

        /// <summary>
        ///     (Pro version only) Enables projection of optional noise texture on foam.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Noise texture improves foam visual by additing additional fake details.
        ///     </para>
        ///     <para>
        ///         Foam parameters have no effect outside of Mesh Render mode.
        ///     </para>
        /// </remarks>
        [Tooltip("Enables projection of optional noise texture on foam")]
        public bool EnableFoamTexture = false;

        /// <summary>
        ///     (Pro version only) Repeat period of foam texture.
        /// </summary>
        /// <remarks>
        ///     Foam texture parameters have no effect outside of Mesh Render mode,
        ///     or when <see cref="EnableFoamTexture"/> set to false.
        /// </remarks>
        [Tooltip("The repeation frequency of the foam texture")]
        [Range(1.0f, 100.0f)]
        public float FoamRepeatPeriod = 9.0f;

        /// <summary>
        ///     (Pro version only) Scale of projected foam texture layers.
        /// </summary>
        /// <remarks>
        ///     Foam texture parameters have no effect outside of Mesh Render mode,
        ///     or when <see cref="EnableFoamTexture"/> set to false.
        /// </remarks>
        [Tooltip("Scale of projected foam texture layers")]
        [Range(0.05f, 20.0f)]
        public float FoamScale = 2.0f;

        /// <summary>
        ///     (Pro version only) Contrast of projected foam texture.
        /// </summary>
        /// <remarks>
        ///     Foam texture parameters have no effect outside of Mesh Render mode,
        ///     or when <see cref="EnableFoamTexture"/> set to false.
        /// </remarks>
        [Tooltip("Contrast of projected foam texture")]
        [Range(0.0f, 3.0f)]
        public float FoamAmplitude = 1.0f;

        /// <summary>
        ///     (Pro version only) Contrast between different layers of noise texture.
        /// </summary>
        /// <remarks>
        ///     Foam texture parameters have no effect outside of Mesh Render mode,
        ///     or when <see cref="EnableFoamTexture"/> set to false.
        /// </remarks>
        [Tooltip("Contrast between different layers of noise texture")]
        [Range(0.1f, 5.0f)]
        public float FoamFBM = 2.0f;

        /// <summary>
        ///     (Pro version only) Controls how much foam info does particle get from the grid.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Higher values makes particles get foam values closer to the avarage value.
        ///         Which blurs foam values.
        ///         Also makes foam dissapear as a result.
        ///     </para>
        ///     <para>
        ///         Foam texture parameters have no effect outside of Mesh Render mode,
        ///         or when <see cref="EnableFoamTexture"/> set to false.
        ///     </para>
        /// </remarks>
        [Tooltip("Controls how much foam info does particle get from the grid")]
        [Range(0.0f, 1.0f)]
        public float FoamBlurring = 0.0001f;

        /// <summary>
        ///     (Pro version only) Additional Material 1.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You can use it by using Material1 parameter of particle species.
        ///     </para>
        ///     <para>
        ///         Multi-material parameters have no effect outside of Mesh Render mode.
        ///     </para>
        /// </remarks>
        public LiquidMaterial Material1 = new LiquidMaterial();

        /// <summary>
        ///     (Pro version only) Additional Material 2.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You can use it by using Material2 parameter of particle species.
        ///     </para>
        ///     <para>
        ///         Multi-material parameters have no effect outside of Mesh Render mode.
        ///     </para>
        /// </remarks>
        public LiquidMaterial Material2 = new LiquidMaterial();

        /// <summary>
        ///     (Pro version only) Additional Material 3.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You can use it by using Material3 parameter of particle species.
        ///     </para>
        ///     <para>
        ///         Multi-material parameters have no effect outside of Mesh Render mode.
        ///     </para>
        /// </remarks>
        public LiquidMaterial Material3 = new LiquidMaterial();
#endif
#endregion
#region Deprecated
        /// @cond SHOW_DEPRECATED
        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("RefractionColor is deprecated. Use Color instead.", true)]
        public Color RefractionColor;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [HideInInspector]
        [Obsolete(
            "Smoothness is deprecated. Use Roughness instead. Roughness have inverted scale, i.e. Smoothness = 1.0 is equivalent to Roughness = 0.0",
            true)]
        public float Smoothness = 0.96f;

        /// Only used for backwards compatibility
        [SerializeField]
        [HideInInspector]
        [FormerlySerializedAs("Smoothness")]
        private float SmoothnessOld = 0.96f;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("Metal is deprecated. Use Metalness instead.", true)]
        public float Metal;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("Opacity is deprecated. Use AbsorptionAmount instead.", true)]
        public float Opacity;

        /// @deprecated
        /// Only used for backwards compatibility
        [HideInInspector]
        [Obsolete("Shadowing is deprecated. We currently don't have correct shadowing effect.", true)]
        public float Shadowing;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("RefractionDistort is deprecated. Use RefractionDistortion instead.", true)]
        public float RefractionDistort;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete(
            "RefractionDistortion is deprecated. Use IndexOfRefraction instead. Note that it have different scale.",
            true)]
        public float RefractionDistortion;

#if ZIBRA_LIQUID_PRO_VERSION
        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("Foam is deprecated. Use FoamIntensity instead.", true)]
        public float Foam;

        /// @deprecated
        /// Only used for backwards compatibility
        [NonSerialized]
        [Obsolete("FoamDensity is deprecated. Use FoamAmount instead.", true)]
        public float FoamDensity;
#endif
        /// @endcond
#endregion
#region Implementation details
        [HideInInspector]
        [SerializeField]
        internal Material NoOpMaterial;

        [HideInInspector]
        [SerializeField]
        private int ObjectVersion = 1;

// Not defined in release versions of the plugin
#if ZIBRA_LIQUID_DEBUG
        [NonSerialized]
        internal float NeuralSamplingDistance = 1.0f;
        [NonSerialized]
        internal float SDFDebug = 0.0f;
#endif

#if UNITY_EDITOR
        private static string DEFAULT_UPSCALE_MATERIAL_GIUD = "374557399a8cb1b499aee6a0cc226496";
        private static string DEFAULT_FLUID_MESH_MATERIAL_GIUD = "248b1858901577949a18bb8d09cb583f";
        private static string DEFAULT_SDF_RENDER_MATERIAL_GIUD = "a29ad26b5c6c24c43ba0cbdc686b6b41";
        private static string NO_OP_MATERIAL_GIUD = "248b1858901577949a18bb8d09cb583f";

        private void OnSceneOpened(Scene scene, UnityEditor.SceneManagement.OpenSceneMode mode)
        {
            Debug.Log("Zibra Liquid Material Parameters format was updated. Please resave scene.");
            UnityEditor.EditorUtility.SetDirty(gameObject);
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened -= OnSceneOpened;
        }
#endif

        [ExecuteInEditMode]
        private void Awake()
        {
            // If Material Parameters is in old format we need to parse old parameters and come up with equivalent new
            // ones
#if UNITY_EDITOR
            bool updated = false;
#endif

            if (ObjectVersion == 1)
            {
                Roughness = 1 - SmoothnessOld;

                ObjectVersion = 2;
#if UNITY_EDITOR
                updated = true;
#endif
            }

            if (ObjectVersion == 2)
            {
                Solver.ZibraLiquid instance = GetComponent<Solver.ZibraLiquid>();

                // if not a newly created liquid instance
                //(material parameters are created before liquid)
                if (instance != null)
                {
                    const float TotalScale = 0.33f;
                    float SimulationScale =
                        TotalScale * (instance.ContainerSize.x + instance.ContainerSize.y + instance.ContainerSize.z);

                    ScatteringAmount *= SimulationScale;
                    AbsorptionAmount *= SimulationScale;
                }

                ObjectVersion = 3;

#if UNITY_EDITOR
                updated = true;
#endif
            }

#if UNITY_EDITOR
            if (updated)
            {
                // Can't mark object dirty in Awake, since scene is not fully loaded yet
                UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += OnSceneOpened;
            }
#endif
        }

#if UNITY_EDITOR
        private void Reset()
        {
            ObjectVersion = 3;
            string DefaultUpscaleMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_UPSCALE_MATERIAL_GIUD);
            UpscaleMaterial = AssetDatabase.LoadAssetAtPath(DefaultUpscaleMaterialPath, typeof(Material)) as Material;
            string DefaultFluidMeshMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_FLUID_MESH_MATERIAL_GIUD);
            FluidMeshMaterial =
                AssetDatabase.LoadAssetAtPath(DefaultFluidMeshMaterialPath, typeof(Material)) as Material;
            string DefaultSDFRenderMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_SDF_RENDER_MATERIAL_GIUD);
            SDFRenderMaterial =
                AssetDatabase.LoadAssetAtPath(DefaultSDFRenderMaterialPath, typeof(Material)) as Material;
            string NoOpMaterialPath = AssetDatabase.GUIDToAssetPath(NO_OP_MATERIAL_GIUD);
            NoOpMaterial = AssetDatabase.LoadAssetAtPath(NoOpMaterialPath, typeof(Material)) as Material;
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened -= OnSceneOpened;
        }

        private void OnValidate()
        {
            if (UpscaleMaterial == null)
            {
                string DefaultUpscaleMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_UPSCALE_MATERIAL_GIUD);
                UpscaleMaterial =
                    AssetDatabase.LoadAssetAtPath(DefaultUpscaleMaterialPath, typeof(Material)) as Material;
            }
            if (FluidMeshMaterial == null)
            {
                string DefaultFluidMeshMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_FLUID_MESH_MATERIAL_GIUD);
                FluidMeshMaterial =
                    AssetDatabase.LoadAssetAtPath(DefaultFluidMeshMaterialPath, typeof(Material)) as Material;
            }
            if (SDFRenderMaterial == null)
            {
                string DefaultSDFRenderMaterialPath = AssetDatabase.GUIDToAssetPath(DEFAULT_SDF_RENDER_MATERIAL_GIUD);
                SDFRenderMaterial =
                    AssetDatabase.LoadAssetAtPath(DefaultSDFRenderMaterialPath, typeof(Material)) as Material;
            }
            string NoOpMaterialPath = AssetDatabase.GUIDToAssetPath(NO_OP_MATERIAL_GIUD);
            NoOpMaterial = AssetDatabase.LoadAssetAtPath(NoOpMaterialPath, typeof(Material)) as Material;
        }
#endif
#endregion
    }
}