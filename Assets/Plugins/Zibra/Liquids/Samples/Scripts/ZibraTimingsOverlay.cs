using UnityEngine;
using System.Collections;
using com.zibra.liquid.Solver;

#if ZIBRA_LIQUID_PROFILING_ENABLED

internal class ZibraTimingsOverlay : MonoBehaviour
{
    public Vector2 scrollPosition = Vector2.zero;
    private string[] DebugEventNames = new string[] { "StepPhysics",
                                                      "PhysicsMPMMLS",
                                                      "MeshGeneration",
                                                      "ZibraLiquidDraw",
                                                      "RenderSDF",
                                                      "DrawMesh",
                                                      "LiquidRayMarch",
                                                      "ClearIDGrid",
                                                      "UpdateGridSteps",
                                                      "MeshSmoothing",
                                                      "SortParticles",
                                                      "InitializeParticles",
                                                      "ApplyManipulators",
                                                      "ComputeVertexProperties",
                                                      "GenerateIndexBuffer",
                                                      "CopyToUnityMesh",
                                                      "ClearGrid",
                                                      "DualContouring",
                                                      "UpdateParticleCount",
                                                      "ParticleToGridSteps",
                                                      "IndexUpdate",
                                                      "CopyParticles",
                                                      "NodeToParticles",
                                                      "RadixSort",
                                                      "Compute3DTextures",
                                                      "BlurNormalCompute",
                                                      "CopyMass",
                                                      "ProcessAnalyticColliders",
                                                      "ProcessNeuralColliders",
                                                      "ProcessGroupColliders",
                                                      "GridToParticle",
                                                      "G2PUpdateAffineVel",
                                                      "Blur",
                                                      "BlurX",
                                                      "BlurY",
                                                      "BlurZ",
                                                      "SaveAPIState",
                                                      "RestoreAPIState",
                                                      "ParticleToGrid0",
                                                      "ParticleToGrid1",
                                                      "UpdateVertices",
                                                      "UpdateRadixSortParameters",
                                                      "UpdateIndirectArguments",
                                                      "UpdateSimulationParameters",
                                                      "UpdateRenderParameters",
                                                      "WriteIndirectArguments",
                                                      "UnknownEvevnt" };
    private GUIStyle currentStyle = null;
    void OnGUI()
    {
        const int SCROLLVIEW_VIEWPORT_MARGIN = 10;
        const int SCROLLVIEW_VIEWPORT_WIDTH = 300;
        const int SCROLLVIEW_VIEWPORT_HEIGHT = 400;

        const int BOX_WIDTH = 275;
        const int BOX_HEIGHT = 25;
        if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Vulkan)
        {
            GUI.Box(new Rect(Screen.width - SCROLLVIEW_VIEWPORT_WIDTH - SCROLLVIEW_VIEWPORT_MARGIN,
                             SCROLLVIEW_VIEWPORT_MARGIN, BOX_WIDTH, BOX_HEIGHT),
                    $"Stats unavailable");
            return;
        }
        InitStyles();

#if UNITY_ANDROID && !UNITY_EDITOR
        GUI.skin.verticalScrollbar.fixedWidth = 50;
        GUI.skin.verticalScrollbarThumb.fixedWidth = 50;
#endif
        scrollPosition = GUI.BeginScrollView(
            new Rect(Screen.width - SCROLLVIEW_VIEWPORT_WIDTH - SCROLLVIEW_VIEWPORT_MARGIN, SCROLLVIEW_VIEWPORT_MARGIN,
                     SCROLLVIEW_VIEWPORT_WIDTH, SCROLLVIEW_VIEWPORT_HEIGHT),
            scrollPosition, new Rect(0, 0, 230, BOX_HEIGHT * DebugEventNames.Length));

        int START_X = 0, x = 0, y = 0;
        ZibraLiquid[] components = FindObjectsOfType<ZibraLiquid>();
        foreach (var liquidInstance in components)
        {
            if (!liquidInstance.isActiveAndEnabled)
                continue;
            GUI.Box(new Rect(START_X + x * BOX_WIDTH, y++ * BOX_HEIGHT, BOX_WIDTH, BOX_HEIGHT),
                    $"Liquid Instance: {liquidInstance.name}", currentStyle);
            for (int i = 0; i < liquidInstance.DebugTimestampsItemsCount; i++)
            {
                var eventName = DebugEventNames[liquidInstance.DebugTimestampsItems[i].EventType];
                var timeVal = liquidInstance.DebugTimestampsItems[i].ExecutionTime.ToString("0.00");
                GUI.Box(new Rect(START_X + x * BOX_WIDTH, y++ * BOX_HEIGHT, BOX_WIDTH, BOX_HEIGHT),
                        $"{eventName}: {timeVal}ms.", currentStyle);
            }
        }
        GUI.EndScrollView();
    }

    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.8f));
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}

#endif
