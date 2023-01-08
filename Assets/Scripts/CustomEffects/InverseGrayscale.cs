using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom Post-processing/InverseGrayscale", typeof(UniversalRenderPipeline))]
public class InverseGrayscale : CustomVolumeComponent
{
    private const string SHADER_NAME = "Hidden/Postprocess/InverseGrayscale";
    private const string PROPERTY_AMOUNT = "_Amount";
    
    private Material _material;

    public ClampedFloatParameter amount = new ClampedFloatParameter(0f, 0f, 1f);
    
    public override bool IsActive()
    {
        if (IsEnable.value == false) return false;
        if (!active || 
            !_material || 
            amount.value <= 0.0f) return false;
        return true;
    }

    public override void Setup()
    {
        if (!_material)
        {
            Shader shader = Shader.Find(SHADER_NAME);
            _material = CoreUtils.CreateEngineMaterial(shader);
        }
    }

    public override void Destroy()
    {
        if (_material)
        {
            CoreUtils.Destroy(_material);
            _material = null;
        }
    }

    public override void Render(CommandBuffer commandBuffer, ref RenderingData renderingData, RenderTargetIdentifier source, RenderTargetIdentifier destination)
    {
        if (!_material) return;
        
        _material.SetFloat(PROPERTY_AMOUNT, amount.value);
        commandBuffer.Blit(source, destination, _material);
    }


}
