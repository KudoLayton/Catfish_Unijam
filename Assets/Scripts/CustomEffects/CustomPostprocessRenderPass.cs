using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostprocessRenderPass : ScriptableRenderPass
{
    private const string TEMP_BUFFER_NAME_1 = "_TempColorBuffer_1";
    private const string TEMP_BUFFER_NAME_2 = "_TempColorBuffer_2";
    private string RenderTag { get; }
    
    private List<CustomVolumeComponent> _customVolumeComponents;
    private List<CustomVolumeComponent> _activeCustomVolumeComponents;
    
    private RenderTargetHandle _source;
    private RenderTargetHandle _tempTexture_1;
    private RenderTargetHandle _tempTexture_2;
    
    public CustomPostprocessRenderPass(string renderTag, RenderPassEvent passEvent, List<CustomVolumeComponent> customVolumeComponents)
    {
        _activeCustomVolumeComponents = new List<CustomVolumeComponent>();
        
        RenderTag = renderTag;
        renderPassEvent = passEvent;
        _customVolumeComponents = customVolumeComponents;
    }

    public virtual void Setup(in RenderTargetIdentifier source)
    {
        _source = new RenderTargetHandle(source);
        _tempTexture_1.Init(TEMP_BUFFER_NAME_1);
        _tempTexture_2.Init(TEMP_BUFFER_NAME_2);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // 렌더링 가능한 상태인지 체크
        if (!renderingData.cameraData.postProcessEnabled ||
            _customVolumeComponents==null ||
            _customVolumeComponents.Count<=0) return;
        
        // 활성화된 customVolumeComponent 리스트 참조
        int stackCount = 0;
        bool isDoubleBuffering = false;
        _activeCustomVolumeComponents.Clear();
        VolumeStack volumeStack = VolumeManager.instance.stack;
        for (int i = 0; i < _customVolumeComponents.Count; i++)
        {
            CustomVolumeComponent component = volumeStack.GetComponent(_customVolumeComponents[i].GetType()) as CustomVolumeComponent;
            if(component) component.Setup();
            if(!component || !component.IsActive()) continue;
            _activeCustomVolumeComponents.Add(component);
            stackCount++;
        }
        if (stackCount == 0) return;
        if (stackCount > 1) isDoubleBuffering = true;
        
        
        CommandBuffer commandBuffer = CommandBufferPool.Get(RenderTag);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();

        // 렌더 텍스처 생성
        CameraData cameraData = renderingData.cameraData;
        RenderTextureDescriptor descriptor = new RenderTextureDescriptor(cameraData.camera.scaledPixelWidth, cameraData.camera.scaledPixelHeight);
        descriptor.colorFormat = cameraData.isHdrEnabled ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
        
        commandBuffer.GetTemporaryRT(_tempTexture_1.id, descriptor);
        // 카메라 텍스처가 없을때 빈 RT 생성
        if (_source != RenderTargetHandle.CameraTarget && !_source.HasInternalRenderTargetId())
        {
            commandBuffer.GetTemporaryRT(_source.id, descriptor);
        }
        
        RenderTargetIdentifier tempBuffer1 = _tempTexture_1.id;
        RenderTargetIdentifier tempBuffer2;
        
        if (!isDoubleBuffering && stackCount==1)
        {
            // CustomVolumeComponent 1개 렌더링
            _activeCustomVolumeComponents[0].Render(commandBuffer, ref renderingData, _source.Identifier(), tempBuffer1);
        }
        else
        {
            // CustomVolumeComponent 2개 이상 렌더링
            // 2번째 임시 렌더버퍼 생성 후 더블 버퍼링 방식으로 Volume을 교차하며 렌더링
            commandBuffer.GetTemporaryRT(_tempTexture_2.id, descriptor);
            tempBuffer2 = _tempTexture_2.id;
            commandBuffer.Blit(_source.Identifier(), tempBuffer1);

            for (int i = 0; i < stackCount; i++)
            {
                CustomVolumeComponent component = _activeCustomVolumeComponents[i];
                if (!component) continue;
                component.Render(commandBuffer, ref renderingData, tempBuffer1, tempBuffer2);
                CoreUtils.Swap(ref tempBuffer1, ref tempBuffer2);
            }
        }
        
        commandBuffer.Blit(tempBuffer1, _source.Identifier());
        
        commandBuffer.ReleaseTemporaryRT(_tempTexture_1.id);
        if (isDoubleBuffering) commandBuffer.ReleaseTemporaryRT(_tempTexture_2.id);
        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }
}