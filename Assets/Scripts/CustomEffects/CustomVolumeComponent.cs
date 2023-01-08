using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public abstract class CustomVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public BoolParameter IsEnable = new BoolParameter(false);
    
    public virtual bool IsTileCompatible() => false;
    public abstract bool IsActive();
    public abstract void Setup();
    public abstract void Destroy();
    public abstract void Render(CommandBuffer commandBuffer, ref RenderingData renderingData, RenderTargetIdentifier source, RenderTargetIdentifier destination);
   
    protected override void OnDestroy()
    {
        //Destroy();
    }
}
