using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom Post-processing/Concentration Line", typeof(UniversalRenderPipeline))]
public class ConcentrationLine : CustomVolumeComponent
{
    private const string SHADER_NAME = "Hidden/Postprocess/Concentration Line";
    private const string PROPERTY_LINETEXTURE = "_LineTex";
    private const string PROPERTY_LINETRANSITIONSPEED = "_LineTransitionSpeed";

    private Material mat;

    public TextureParameter lineTexture = new TextureParameter(null);
    public ClampedFloatParameter lineTransitionSpeed = new ClampedFloatParameter(0f, 0f, 10f);
    
    public override bool IsActive()
    {
        if (IsEnable.value == false) return false;
        if (!active || 
            !mat || 
            lineTransitionSpeed.value <= 0.0f) return false;
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
        
        mat.SetFloat(PROPERTY_LINETRANSITIONSPEED, lineTransitionSpeed.value);
        mat.SetTexture(PROPERTY_LINETEXTURE, lineTexture.value);
        commandBuffer.Blit(source, destination, mat);
    }


}