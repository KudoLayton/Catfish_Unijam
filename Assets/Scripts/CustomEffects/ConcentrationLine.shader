Shader "Hidden/Postprocess/Concentration Line"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_LineTex ("Line Texture", 2D) = "black" {}
		_LineTransitionSpeed ("Line Transition Speed", float) = 0
	}
	SubShader
	{
	    Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
		ZTest Always ZWrite Off Cull Off

        Pass
        {
            HLSLPROGRAM

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"

            #pragma prefer_hlslcc gles
            
            #pragma vertex FullscreenVert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _LineTex;
            float4 _LineTex_ST;

            float _LineTransitionSpeed;
             
       
            float4 frag(Varyings i) : SV_Target
            {
                float2 mainTexUV = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                float4 col = tex2D(_MainTex, mainTexUV);
            	float4 lineTex = tex2D(_LineTex, mainTexUV);
            	float3 lines = lineTex.rgb * lineTex.a;
				int currentLine = (_LineTransitionSpeed * _Time.y) % 3;
            	float a = lines[currentLine];
            	col.rgb = col.rgb * (1 - a);
                
                return col;
            }
            
            ENDHLSL
        }
    }
}