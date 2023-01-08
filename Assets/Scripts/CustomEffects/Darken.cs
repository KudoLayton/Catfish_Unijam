using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom Post-processing/Darken", typeof(UniversalRenderPipeline))]
public class Darken : CustomVolumeComponent
{
    private const string SHADER_NAME = "Hidden/Postprocess/Darken";
    private const string PROPERTY_AMOUNT = "_Amount";
    
    private Material mat;

    public ClampedFloatParameter amount = new ClampedFloatParameter(0f, 0f, 1f);
    
    public override bool IsActive()
    {
        if (IsEnable.value == false) return false;
        if (!active || 
            !mat || 
            amount.value <= 0.0f) return false;
        return true;
    }

    public override void Setup()
    {
        if (!mat)
        {
            Shader shader = Shader.Find(SHADER_NAME);
            mat = CoreUtils.CreateEngineMaterial(shader);
        }
    }

    public override void Destroy()
    {
        if (mat)
        {
            CoreUtils.Destroy(mat);
            mat = null;
        }
    }

    public override void Render(CommandBuffer commandBuffer, ref RenderingData renderingData, RenderTargetIdentifier source, RenderTargetIdentifier destination)
    {
        if (!mat) return;
        
        mat.SetFloat(PROPERTY_AMOUNT, amount.value);
        commandBuffer.Blit(source, destination, mat);
    }


}