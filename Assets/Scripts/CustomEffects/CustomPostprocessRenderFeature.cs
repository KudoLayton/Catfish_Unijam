using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostprocessRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public string renderTag = "CustomPostprocessRender";
        public RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }
    
    [SerializeField] private Settings _settings;
    private CustomPostprocessRenderPass _renderPass;
    private List<CustomVolumeComponent> _customVolumeComponents;
    private UniversalRendererData _universalRendererData;
    
    public override void Create()
    {
        InitVolumeComponents(ref _customVolumeComponents);

        _universalRendererData = RendererUtill.GetUniversalRendererData();
        _renderPass = new CustomPostprocessRenderPass(_settings.renderTag, _settings.passEvent, _customVolumeComponents);
    }
    
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        _universalRendererData = null;
        ClearVolumeComponents();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(!_universalRendererData) _universalRendererData = RendererUtill.GetUniversalRendererData();
        if(!IsPostProcessEnabled(_universalRendererData, ref renderingData)) return;
       
        _renderPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(_renderPass);
    }

    // Postprocess 옵션 활성화 체크
    private bool IsPostProcessEnabled(UniversalRendererData universalRendererData, ref RenderingData renderingData)
    {
        // 카메라 오브젝트의 Post Processing 활성화 체크
        if (!renderingData.cameraData.postProcessEnabled) return false;
        // RendererData 에셋의 Post-processing 활성화 체크
        if(!_universalRendererData ||
           !_universalRendererData.postProcessData) return false;

        return true;
    }
    
    // VolumeStack에서 CustomVolumeComponent 클래스 리스트 참조
    public bool InitVolumeComponents(ref List<CustomVolumeComponent> refComponents)
    {
        List<CustomVolumeComponent> components = new List<CustomVolumeComponent>();
        VolumeStack stack = VolumeManager.instance.stack;
        
        // 프로젝트 내의 모든 VolumeComponent 클래스 참조
        System.Type[] types = VolumeManager.instance.baseComponentTypeArray;
        
        for (int i = 0; i < types.Length; i++)
        {
            System.Type type = types[i];
            
            // 타입 검사
            if(!type.IsSubclassOf(typeof(CustomVolumeComponent))) continue;
            
            // VolumeStack에서 CustomVolumeComponent 참조
            CustomVolumeComponent component = stack.GetComponent(type) as CustomVolumeComponent;
            if(!component) continue;
            component.Setup();
            components.Add(component);
        }
        
        // CustomVolumeComponent참조하지 못했을때 리턴
        if (components.Count <= 0) return false;
        
        refComponents = components;
        return true;
    }
    
    public void ClearVolumeComponents()
    {
        if (_customVolumeComponents == null) return;
        for (int i = 0; i < _customVolumeComponents.Count; i++)
        {
            _customVolumeComponents[i].Destroy();
        }
    }


}


