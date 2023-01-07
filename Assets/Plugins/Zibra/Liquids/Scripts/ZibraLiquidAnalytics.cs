#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Networking;
using com.zibra.liquid.Solver;
using com.zibra.liquid.Manipulators;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Utilities;
using com.zibra.liquid.Editor.SDFObjects;

namespace com.zibra.liquid.Analytics
{
    [InitializeOnLoad]
    public static class ZibraLiquidAnalyticsData
    {
        const int REPORT_PERIOD = 12 * 60 * 60;  // 12 hours
        const string ENGINE = "Unity";
        const int TRACKING_VERSION = 1;
        const int FRAME_UPDATE_PERIOD = 60 * 60 * 30; // 60 minutes if FPS = 30

        public static void TrackBuiltPlatform(string builtPlatform)
        {
            EditorPrefs.SetBool($"ZibraLiquidsTracking_Built{builtPlatform}", true);
        }

        public static void TrackBakedStateSaved()
        {
            EditorPrefs.SetBool("ZibraLiquidsTracking_BakedStateSaved", true);
        }

        public static void TrackLiquidInitialization(ZibraLiquid liquid)
        {
            EditorPrefs.SetBool("ZibraLiquidsTracking_ZibraLiquidsUsed", true);

#if ZIBRA_LIQUID_PAID_VERSION
            if (liquid.InitialState == ZibraLiquid.InitialStateType.BakedLiquidState && liquid.BakedInitialStateAsset != null)
            {
                   EditorPrefs.SetBool("ZibraLiquidsTracking_BakedStateUsed", true);
            }
#endif
            var currentRendererMode = liquid.CurrentRenderingMode;
            switch (currentRendererMode)
            {
                case ZibraLiquid.RenderingMode.MeshRender:
                    EditorPrefs.SetBool("ZibraLiquidsTracking_MashRendererUsed", true);
                    break;
                case ZibraLiquid.RenderingMode.UnityRender:
                    EditorPrefs.SetBool("ZibraLiquidsTracking_UnityRendererUsed", true);
                    break;
            }

            if (liquid.EnableDownscale)
            {
                EditorPrefs.SetBool("ZibraLiquidsTracking_RenderDownscaleUsed", true);
            }

            int currentNodeCount = liquid.GridNodeCount;
            int previousMaxNodeCount = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxNodeCount", 0);
            int previousMinNodeCount = EditorPrefs.GetInt("ZibraLiquidsTracking_MinNodeCount", int.MaxValue);
            EditorPrefs.SetInt("ZibraLiquidsTracking_MaxNodeCount", Math.Max(currentNodeCount, previousMaxNodeCount));
            EditorPrefs.SetInt("ZibraLiquidsTracking_MinNodeCount", Math.Min(currentNodeCount, previousMinNodeCount));

            int currentMaxNumParticles = liquid.MaxNumParticles;
            int previousMaxMaxNumParticles = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxMaxParticleCount", 0);
            int previousMinMaxNumParticles = EditorPrefs.GetInt("ZibraLiquidsTracking_MinMaxParticleCount", int.MaxValue);
            EditorPrefs.SetInt("ZibraLiquidsTracking_MaxMaxParticleCount", Math.Max(currentMaxNumParticles, previousMaxMaxNumParticles));
            EditorPrefs.SetInt("ZibraLiquidsTracking_MinMaxParticleCount", Math.Min(currentMaxNumParticles, previousMinMaxNumParticles));

            int currentGridResolution = liquid.GridResolution;
            int previousMaxGridResolution = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxGridResolution", 0);
            int previousMinGridResolution = EditorPrefs.GetInt("ZibraLiquidsTracking_MinGridResolution", int.MaxValue);
            EditorPrefs.SetInt("ZibraLiquidsTracking_MaxGridResolution", Math.Max(currentGridResolution, previousMaxGridResolution));
            EditorPrefs.SetInt("ZibraLiquidsTracking_MinGridResolution", Math.Min(currentGridResolution, previousMinGridResolution));

            foreach (var item in liquid.GetManipulatorList())
            {
                var manipulatorType = item.GetManipulatorType();
                var sdf = item.GetComponent<SDFObject>();
                
                if (sdf == null)
                    continue;

                switch (manipulatorType)
                {
                    case Manipulator.ManipulatorType.Emitter:
                    case Manipulator.ManipulatorType.Void:
                    case Manipulator.ManipulatorType.ForceField:
                    case Manipulator.ManipulatorType.Detector:
                    case Manipulator.ManipulatorType.SpeciesModifier:
                        TrackManipulatorSDF(sdf, manipulatorType.ToString());
                        break;
                }
            }

            foreach (var item in liquid.GetColliderList())
            {
                var sdf = item.GetComponent<SDFObject>();

                if (sdf == null)
                    continue;

                TrackManipulatorSDF(sdf, "Collider");
            }
        }

        public static void CheckSendPeriod()
        {
            int dateNow = GetCurrentTimeAsUnixTimestamp();

            if (!EditorPrefs.HasKey("ZibraLiquidsTracking_LastSentDate"))
            {
                EditorPrefs.SetInt("ZibraLiquidsTracking_LastSentDate", dateNow);
                return;
            }

            int lastSentDate = EditorPrefs.GetInt("ZibraLiquidsTracking_LastSentDate");

            if (dateNow - lastSentDate > REPORT_PERIOD)
            {
                ZibraLiquidAnalyticsStruct data = GetAnalyticsData();
                string jsonData = JsonUtility.ToJson(data);
                ZibraLiquidAnalyticsSender.SendAnalyticsData(jsonData);
            }
        }

        static int FrameCount = FRAME_UPDATE_PERIOD;

        private static void Update()
        {
            if (FrameCount == FRAME_UPDATE_PERIOD)
            {
                FrameCount = 0;
                CheckSendPeriod();
            }
            else
            {
                FrameCount++;
            }
        }

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            EditorApplication.update += Update;
        }

        private static void TrackManipulatorSDF(SDFObject sdf, string manipulatorType)
        {
            if (sdf is AnalyticSDF)
            {
                EditorPrefs.SetBool($"ZibraLiquidsTracking_Manipulator{manipulatorType}AnalyticSDF", true);
            }
#if ZIBRA_LIQUID_PAID_VERSION
            else if (sdf is NeuralSDF)
            {
                EditorPrefs.SetBool($"ZibraLiquidsTracking_Manipulator{manipulatorType}NeuralSDF", true);
            }
#endif
#if ZIBRA_LIQUID_PRO_VERSION
            else if (sdf is SkinnedMeshSDF)
            {
                EditorPrefs.SetBool($"ZibraLiquidsTracking_Manipulator{manipulatorType}SkinnedMeshSDF", true);
            }
#endif
        }

        // Structure used with JSON parser
        private struct ZibraLiquidAnalyticsStruct
        {
            public string HardwareId;
            public string PluginSKU;
            public string Engine;
            public int TrackingVersion;
            public string PluginVersionNumber;
            public bool ServerKeySaved;
            public string DeveloperOS;
            public string EditorsGraphicsAPI;
            public string EngineVersion;
            public string RenderPipeline;
            public bool BuildPlatformWindows;
            public bool BuildPlatformMacOS;
            public bool BuildPlatformLinux;
            public bool BuildPlatformUWP;
            public bool BuildPlatformAndroid;
            public bool BuildPlatformiOS;
            public bool ZibraLiquidsUsed;
            public bool MeshRenderUsed;
            public bool UnityRenderUsed;
            public bool RenderDownscaleUsed;
            public int MinGridNodesUsed;
            public int MaxGridNodesUsed;
            public int MinMaxParticleCount;
            public int MaxMaxParticleCount;
            public int MinGridResolutionUsed;
            public int MaxGridResolutionUsed;
            public bool ManipulatorEmitterAnalytic;
            public bool ManipulatorEmitterNeural;
            public bool ManipulatorEmitterSkinnedMesh;
            public bool ManipulatorVoidAnalytic;
            public bool ManipulatorVoidNeural;
            public bool ManipulatorVoidSkinnedMesh;
            public bool ManipulatorForceFieldAnalytic;
            public bool ManipulatorForceFieldNeural;
            public bool ManipulatorForceFieldSkinnedMesh;
            public bool ManipulatorColliderAnalytic;
            public bool ManipulatorColliderNeural;
            public bool ManipulatorColliderSkinnedMesh;
            public bool ManipulatorDetectorAnalytic;
            public bool ManipulatorDetectorNeural;
            public bool ManipulatorDetectorSkinnedMesh;
            public bool ManipulatorSpeciesModifierAnalytic;
            public bool ManipulatorSpeciesModifierNeural;
            public bool ManipulatorSpeciesModifierSkinnedMesh;
            public bool InitialBakedStateUsed;
            public bool InitialBakedStateSaved;
        }

        private static ZibraLiquidAnalyticsStruct GetAnalyticsData()
        {
            ZibraLiquidAnalyticsStruct data = new ZibraLiquidAnalyticsStruct();

            data.HardwareId = SystemInfo.deviceUniqueIdentifier;
#if ZIBRA_LIQUID_PRO_VERSION
            data.PluginSKU = "Pro";
#elif ZIBRA_LIQUID_PAID_VERSION
            data.PluginSKU = "Paid";
#else
            data.PluginSKU = "Free";
#endif
            data.Engine = ENGINE;
            data.TrackingVersion = TRACKING_VERSION;
            data.PluginVersionNumber = ZibraLiquid.PluginVersion;
            data.DeveloperOS = SystemInfo.operatingSystemFamily.ToString();
            data.EditorsGraphicsAPI = SystemInfo.graphicsDeviceType.ToString();
            data.EngineVersion = Application.unityVersion.ToString();
            data.RenderPipeline = RenderPipelineDetector.GetRenderPipelineType().ToString();
#if ZIBRA_LIQUID_PAID_VERSION
            data.ServerKeySaved = ZibraServerAuthenticationManager.GetInstance().PluginLicenseKey != "";
#else
            data.ServerKeySaved = false;
#endif
            data.BuildPlatformWindows = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltWindows", false);
            data.BuildPlatformLinux = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltLinux", false);
            data.BuildPlatformMacOS = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltMacOS", false);
            data.BuildPlatformUWP = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltUWP", false);
            data.BuildPlatformAndroid = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltAndroid", false);
            data.BuildPlatformiOS = EditorPrefs.GetBool("ZibraLiquidsTracking_BuiltIOS", false);

            data.ZibraLiquidsUsed = EditorPrefs.GetBool("ZibraLiquidsTracking_ZibraLiquidsUsed", false);

            data.MeshRenderUsed = EditorPrefs.GetBool("ZibraLiquidsTracking_MashRendererUsed", false);
            data.UnityRenderUsed = EditorPrefs.GetBool("ZibraLiquidsTracking_UnityRendererUsed", false);
            data.RenderDownscaleUsed = EditorPrefs.GetBool("ZibraLiquidsTracking_RenderDownscaleUsed", false);

            data.MinGridNodesUsed = EditorPrefs.GetInt("ZibraLiquidsTracking_MinNodeCount", int.MaxValue);
            data.MaxGridNodesUsed = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxNodeCount", 0);
            data.MinMaxParticleCount = EditorPrefs.GetInt("ZibraLiquidsTracking_MinMaxParticleCount", int.MaxValue);
            data.MaxMaxParticleCount = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxMaxParticleCount", 0);
            data.MinGridResolutionUsed = EditorPrefs.GetInt("ZibraLiquidsTracking_MinGridResolution", int.MaxValue);
            data.MaxGridResolutionUsed = EditorPrefs.GetInt("ZibraLiquidsTracking_MaxGridResolution", 0);

            data.ManipulatorEmitterAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorEmitterAnalyticSDF", false);
            data.ManipulatorEmitterNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorEmitterNeuralSDF", false);
            data.ManipulatorEmitterSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorEmitterSkinnedMeshSDF", false);

            data.ManipulatorVoidAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorVoidAnalyticSDF", false);
            data.ManipulatorVoidNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorVoidNeuralSDF", false);
            data.ManipulatorVoidSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorVoidSkinnedMeshSDF", false);

            data.ManipulatorForceFieldAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorForceFieldAnalyticSDF", false);
            data.ManipulatorForceFieldNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorForceFieldNeuralSDF", false);
            data.ManipulatorForceFieldSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorForceFieldSkinnedMeshSDF", false);

            data.ManipulatorColliderAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorColliderAnalyticSDF", false);
            data.ManipulatorColliderNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorColliderNeuralSDF", false);
            data.ManipulatorColliderSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorColliderSkinnedMeshSDF", false);

            data.ManipulatorDetectorAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorDetectorAnalyticSDF", false);
            data.ManipulatorDetectorNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorDetectorNeuralSDF", false);
            data.ManipulatorDetectorSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorDetectorSkinnedMeshSDF", false);

            data.ManipulatorSpeciesModifierAnalytic = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorSpeciesModifierAnalyticSDF", false);
            data.ManipulatorSpeciesModifierNeural = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorSpeciesModifierNeuralSDF", false);
            data.ManipulatorSpeciesModifierSkinnedMesh = EditorPrefs.GetBool("ZibraLiquidsTracking_ManipulatorSpeciesModifierSkinnedMeshSDF", false);

            data.InitialBakedStateUsed = EditorPrefs.GetBool("ZibraLiquidsTracking_BakedStateUsed", false);
            data.InitialBakedStateSaved = EditorPrefs.GetBool("ZibraLiquidsTracking_BakedStateSaved", false);

            return data;
        }

        public static void CleanAnalyticData()
        {
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltWindows");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltMacOS");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltLinux");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltUWP");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltAndroid");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BuiltIOS");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ZibraLiquidsUsed");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MashRendererUsed");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_UnityRendererUsed");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_RenderDownscaleUsed");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MinNodeCount");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MaxNodeCount");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MinMaxParticleCount");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MaxMaxParticleCount");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MinGridResolution");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_MaxGridResolution");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorEmitterAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorEmitterNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorEmitterSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorVoidAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorVoidNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorVoidSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorForceFieldAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorForceFieldNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorForceFieldSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorColliderAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorColliderrNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorColliderSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorDetectorAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorDetectorNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorDetectorSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorSpeciesModifierAnalyticSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorSpeciesModifierNeuralSDF");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_ManipulatorSpeciesModifierSkinnedMeshSDF");

            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BakedStateUsed");
            EditorPrefs.DeleteKey("ZibraLiquidsTracking_BakedStateSaved");

            EditorPrefs.SetInt("ZibraLiquidsTracking_LastSentDate", GetCurrentTimeAsUnixTimestamp());
        }

        public static int GetCurrentTimeAsUnixTimestamp()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }

    public class ZibraLiquidBuildPostProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPostprocessBuild(BuildReport report)
        {
            switch (report.summary.platform)
            {
                case BuildTarget.StandaloneOSX:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("MacOS");
                    break;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("Windows");
                    break;
                case BuildTarget.iOS:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("IOS");
                    break;
                case BuildTarget.Android:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("Android");
                    break;
                case BuildTarget.WSAPlayer:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("UWP");
                    break;
                case BuildTarget.StandaloneLinux64:
                    ZibraLiquidAnalyticsData.TrackBuiltPlatform("Linux");
                    break;
            }
        }
    }
   
    internal static class ZibraLiquidAnalyticsSender
    {
        const string ANALYTIC_API_URL = "https://analytics.zibra.ai/api/pluginUsageAnalytics";

        private static UnityWebRequestAsyncOperation request;

        public static void SendAnalyticsData(string jsonData)
        {
            if (request != null)
            {
                return;
            }

            request = UnityWebRequest.Post(ANALYTIC_API_URL, jsonData).SendWebRequest();
            request.completed += UpdateKeyRequest;
        }

        private static void UpdateKeyRequest(AsyncOperation obj)
        {
            if (request == null || !request.isDone)
            {
                return;
            }

#if UNITY_2020_2_OR_NEWER
            if (request.webRequest.result != UnityWebRequest.Result.Success)
#else
            if (request.webRequest.isHttpError || request.webRequest.isNetworkError)
#endif
            {
                request.webRequest.Dispose();
                request = null;
                return;
            }

            if (request.webRequest.responseCode != 201)
            {
                request.webRequest.Dispose();
                request = null;
                return;
            }

            ZibraLiquidAnalyticsData.CleanAnalyticData();
            request.webRequest.Dispose();
            request = null;
        }
    }
}
#endif

