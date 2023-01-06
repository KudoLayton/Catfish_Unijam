#if UNITY_PIPELINE_URP

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using com.zibra.liquid.Solver;

namespace com.zibra.liquid
{
    /// <summary>
    ///     Component responsible for rendering liquid in case of URP.
    /// </summary>
    /// <remarks>
    ///     This is not used in case liquid's
    ///     <see cref="Solver::ZibraLiquid::CurrentRenderingMode">ZibraLiquid.CurrentRenderingMode</see>
    ///     is set to Unity Render.
    /// </remarks>
    public class LiquidURPRenderComponent : ScriptableRendererFeature
    {
#region Public Interface
        /// <summary>
        ///     URP specific liquid rendering settings.
        /// </summary>
        [System.Serializable]
        public class LiquidURPRenderSettings
        {
            /// <summary>
            ///     Globally defines whether liquid renders in URP.
            /// </summary>
            public bool IsEnabled = true;
            /// <summary>
            ///     Injection point where we will insert liquid rendering.
            /// </summary>
            /// <remarks>
            ///     In case of URP, this parameter will be used instead of
            ///     <see cref="Solver::ZibraLiquid::CurrentInjectionPoint">ZibraLiquid.CurrentInjectionPoint</see>.
            /// </remarks>
            public RenderPassEvent InjectionPoint = RenderPassEvent.AfterRenderingTransparents;
        }

        /// <summary>
        ///     See <see cref="LiquidURPRenderSettings"/>.
        /// </summary>
        // Must be called exactly "settings" so Unity shows this as render feature settings in editor
        public LiquidURPRenderSettings settings = new LiquidURPRenderSettings();

        /// <summary>
        ///     Creates URP ScriptableRendererFeature.
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        ///     Adds scriptable render passes.
        /// </summary>
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!settings.IsEnabled)
            {
                return;
            }

            if (renderingData.cameraData.cameraType != CameraType.Game &&
                renderingData.cameraData.cameraType != CameraType.SceneView)
            {
                return;
            }

            Camera camera = renderingData.cameraData.camera;
            camera.depthTextureMode = DepthTextureMode.Depth;

            int liquidsToRenderCount = 0;
            int backgroundsToCopyCount = 0;
            int liquidsToUpscaleCount = 0;

            foreach (var liquid in ZibraLiquid.AllFluids)
            {
                if (liquid != null && liquid.Initialized)
                {
                    liquidsToRenderCount++;
                    if (liquid.EnableDownscale)
                    {
                        liquidsToUpscaleCount++;
                    }
                    if (liquid.IsBackgroundCopyNeeded(camera))
                    {
                        backgroundsToCopyCount++;
                    }
                }
            }

            if (copyPasses == null || copyPasses.Length != backgroundsToCopyCount)
            {
                copyPasses = new CopyBackgroundURPRenderPass[backgroundsToCopyCount];
                for (int i = 0; i < backgroundsToCopyCount; ++i)
                {
                    copyPasses[i] = new CopyBackgroundURPRenderPass(settings.InjectionPoint);
                }
            }

            if (liquidNativePasses == null || liquidNativePasses.Length != liquidsToRenderCount)
            {
                liquidNativePasses = new LiquidNativeRenderPass[liquidsToRenderCount];
                for (int i = 0; i < liquidsToRenderCount; ++i)
                {
                    liquidNativePasses[i] = new LiquidNativeRenderPass(settings.InjectionPoint);
                }
            }

            if (liquidURPPasses == null || liquidURPPasses.Length != liquidsToRenderCount)
            {
                liquidURPPasses = new LiquidURPRenderPass[liquidsToRenderCount];
                for (int i = 0; i < liquidsToRenderCount; ++i)
                {
                    liquidURPPasses[i] = new LiquidURPRenderPass(settings.InjectionPoint);
                }
            }

            if (upscalePasses == null || upscalePasses.Length != liquidsToUpscaleCount)
            {
                upscalePasses = new LiquidUpscaleURPRenderPass[liquidsToUpscaleCount];
                for (int i = 0; i < liquidsToUpscaleCount; ++i)
                {
                    upscalePasses[i] = new LiquidUpscaleURPRenderPass(settings.InjectionPoint);
                }
            }

            int currentCopyPass = 0;
            int currentLiquidPass = 0;
            int currentUpscalePass = 0;

            foreach (var liquid in ZibraLiquid.AllFluids)
            {
                if (liquid != null && liquid.IsRenderingEnabled() &&
                    ((camera.cullingMask & (1 << liquid.gameObject.layer)) != 0))
                {
                    if (liquid.IsBackgroundCopyNeeded(camera))
                    {
                        copyPasses[currentCopyPass].liquid = liquid;

#if UNITY_PIPELINE_URP_10_0_OR_HIGHER
                        copyPasses[currentCopyPass].ConfigureInput(ScriptableRenderPassInput.Color |
                                                                   ScriptableRenderPassInput.Depth);
#endif
                        copyPasses[currentCopyPass].renderPassEvent = settings.InjectionPoint;

                        renderer.EnqueuePass(copyPasses[currentCopyPass]);
                        currentCopyPass++;
                    }

                    liquidNativePasses[currentLiquidPass].liquid = liquid;
                    liquidNativePasses[currentLiquidPass].renderPassEvent = settings.InjectionPoint;
                    renderer.EnqueuePass(liquidNativePasses[currentLiquidPass]);

                    liquidURPPasses[currentLiquidPass].liquid = liquid;
#if UNITY_PIPELINE_URP_10_0_OR_HIGHER
                    liquidURPPasses[currentLiquidPass].ConfigureInput(ScriptableRenderPassInput.Color |
                                                                      ScriptableRenderPassInput.Depth);
#endif

#if !UNITY_PIPELINE_URP_9_0_OR_HIGHER
                    liquidURPPasses[currentLiquidPass].Setup(renderer, ref renderingData);
#endif
                    liquidURPPasses[currentLiquidPass].renderPassEvent = settings.InjectionPoint;

                    renderer.EnqueuePass(liquidURPPasses[currentLiquidPass]);
                    currentLiquidPass++;
                    if (liquid.EnableDownscale)
                    {
                        upscalePasses[currentUpscalePass].liquid = liquid;

                        upscalePasses[currentUpscalePass].renderPassEvent = settings.InjectionPoint;

                        renderer.EnqueuePass(upscalePasses[currentUpscalePass]);
                        currentUpscalePass++;
                    }
                }
            }
        }
#endregion
#region Implementation details
        private class CopyBackgroundURPRenderPass : ScriptableRenderPass
        {
            public ZibraLiquid liquid;

            RenderTargetIdentifier cameraColorTexture;

            public CopyBackgroundURPRenderPass(RenderPassEvent injectionPoint)
            {
                renderPassEvent = injectionPoint;
            }

#if UNITY_PIPELINE_URP_9_0_OR_HIGHER
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                cameraColorTexture = renderingData.cameraData.renderer.cameraColorTarget;
            }
#else
            public void Setup(ScriptableRenderer renderer, ref RenderingData renderingData)
            {
                cameraColorTexture = renderer.cameraColorTarget;
            }
#endif

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                Camera camera = renderingData.cameraData.camera;

                CommandBuffer cmd = CommandBufferPool.Get("ZibraLiquid.Render");

                if (liquid.CameraResourcesMap.ContainsKey(camera))
                {
#if UNITY_PIPELINE_URP_9_0_OR_HIGHER
                    Blit(cmd, cameraColorTexture, liquid.CameraResourcesMap[camera].Background);
#else
                    // For some reason old version of URP don't want to blit texture via correct API
                    cmd.Blit(cameraColorTexture, liquid.CameraResourcesMap[camera].Background);
#endif
                }

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        private class LiquidNativeRenderPass : ScriptableRenderPass
        {
            public ZibraLiquid liquid;

            public LiquidNativeRenderPass(RenderPassEvent injectionPoint)
            {
                renderPassEvent = injectionPoint;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                Camera camera = renderingData.cameraData.camera;
                camera.depthTextureMode = DepthTextureMode.Depth;
                CommandBuffer cmd = CommandBufferPool.Get("ZibraLiquid.Render");

                liquid.RenderCallBack(renderingData.cameraData.camera, renderingData.cameraData.renderScale);

                // set initial parameters in the native plugin
                ZibraLiquidBridge.SubmitInstanceEvent(cmd, liquid.CurrentInstanceID,
                                                      ZibraLiquidBridge.EventID.SetCameraParams,
                                                      liquid.CamNativeParams[camera]);

                if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Vulkan)
                {
                    cmd.SetRenderTarget(liquid.Color0, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store,
                                        liquid.Depth, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
                    cmd.ClearRenderTarget(true, true, Color.clear);
                }

                liquid.RenderLiquidNative(cmd, renderingData.cameraData.camera);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        private class LiquidURPRenderPass : ScriptableRenderPass
        {
            public ZibraLiquid liquid;

            RenderTargetIdentifier cameraColorTexture;

            static int UpscaleColorTextureID = Shader.PropertyToID("ZibraLiquid_LiquidTempColorTexture");
            RenderTargetIdentifier UpscaleColorTexture;

            public LiquidURPRenderPass(RenderPassEvent injectionPoint)
            {
                renderPassEvent = injectionPoint;
            }

#if UNITY_PIPELINE_URP_9_0_OR_HIGHER
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                cameraColorTexture = renderingData.cameraData.renderer.cameraColorTarget;
            }
#else
            public void Setup(ScriptableRenderer renderer, ref RenderingData renderingData)
            {
                cameraColorTexture = renderer.cameraColorTarget;
            }
#endif

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                if (liquid.EnableDownscale)
                {
                    RenderTextureDescriptor descriptor = cameraTextureDescriptor;

                    Vector2Int dimensions = new Vector2Int(descriptor.width, descriptor.height);
                    dimensions = liquid.ApplyDownscaleFactor(dimensions);
                    descriptor.width = dimensions.x;
                    descriptor.height = dimensions.y;

                    descriptor.msaaSamples = 1;

                    descriptor.colorFormat = RenderTextureFormat.ARGBHalf;
                    descriptor.depthBufferBits = 0;

                    cmd.GetTemporaryRT(UpscaleColorTextureID, descriptor, FilterMode.Bilinear);

                    UpscaleColorTexture = new RenderTargetIdentifier(UpscaleColorTextureID);
                    ConfigureTarget(UpscaleColorTexture);
                    ConfigureClear(ClearFlag.All, new Color(0, 0, 0, 0));
                }
                else
                {
                    ConfigureTarget(cameraColorTexture);
                    // ConfigureClear seems to be persistent, so need to reset it
                    ConfigureClear(ClearFlag.None, new Color(0, 0, 0, 0));
                }
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                Camera camera = renderingData.cameraData.camera;
                camera.depthTextureMode = DepthTextureMode.Depth;
                CommandBuffer cmd = CommandBufferPool.Get("ZibraLiquid.Render");

                if (!liquid.EnableDownscale)
                {
                    cmd.SetRenderTarget(cameraColorTexture);
                }

                liquid.RenderLiquidMain(cmd, camera);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

#if UNITY_PIPELINE_URP_9_0_OR_HIGHER
            public override void OnCameraCleanup(CommandBuffer cmd)
#else
            public override void FrameCleanup(CommandBuffer cmd)
#endif
            {
                if (liquid.EnableDownscale)
                {
                    cmd.ReleaseTemporaryRT(UpscaleColorTextureID);
                }
            }
        }

        private class LiquidUpscaleURPRenderPass : ScriptableRenderPass
        {
            public ZibraLiquid liquid;

            static int UpscaleColorTextureID = Shader.PropertyToID("ZibraLiquid_LiquidTempColorTexture");
            RenderTargetIdentifier UpscaleColorTexture;

            public LiquidUpscaleURPRenderPass(RenderPassEvent injectionPoint)
            {
                renderPassEvent = injectionPoint;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                Camera camera = renderingData.cameraData.camera;
                camera.depthTextureMode = DepthTextureMode.Depth;
                CommandBuffer cmd = CommandBufferPool.Get("ZibraLiquid.Render");

                UpscaleColorTexture = new RenderTargetIdentifier(UpscaleColorTextureID);
                liquid.UpscaleLiquidDirect(cmd, camera, UpscaleColorTexture);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        // 1 pass per rendered liquid that requires background copy
        CopyBackgroundURPRenderPass[] copyPasses;
        // 1 pass per rendered liquid
        LiquidNativeRenderPass[] liquidNativePasses;
        // 1 pass per rendered liquid
        LiquidURPRenderPass[] liquidURPPasses;
        // 1 pass per rendered liquid that have downscale enabled
        LiquidUpscaleURPRenderPass[] upscalePasses;
#endregion
    }
}

#endif // UNITY_PIPELINE_URP
