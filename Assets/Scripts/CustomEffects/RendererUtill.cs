 using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class RendererUtill
{
    // 렌더파이프라인 에셋 참조
    public static RenderPipelineAsset GetRenderPipelineAsset()
    {
        return GraphicsSettings.renderPipelineAsset;
    }

    // 렌더데이터 에셋 리스트 참조
    public static ScriptableRendererData[] GetScriptableRendererDatas()
    {
        RenderPipelineAsset pipelineAsset = GetRenderPipelineAsset();
        if (!pipelineAsset) return null;
        
        FieldInfo propertyInfo = pipelineAsset.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
        ScriptableRendererData[] scriptableRendererDatas = propertyInfo.GetValue(pipelineAsset) as ScriptableRendererData[];
        return scriptableRendererDatas;
    }

    // Universal렌더 데이터 참조(기본 세팅)
    public static UniversalRendererData GetUniversalRendererData(int rendererListIndex = 0)
    {
        ScriptableRendererData[] scriptableRendererDatas = GetScriptableRendererDatas();
        if (scriptableRendererDatas == null || scriptableRendererDatas.Length <= 0) return null;
        
        UniversalRendererData universalRendererData = scriptableRendererDatas[rendererListIndex] as UniversalRendererData;
        return universalRendererData;
    }
    
}
