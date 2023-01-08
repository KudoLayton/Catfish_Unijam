// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34401,y:32621,varname:node_3138,prsc:2|normal-7601-RGB,emission-9665-OUT,clip-5741-OUT;n:type:ShaderForge.SFN_Tex2d,id:2500,x:32935,y:32521,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_2500,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5373,x:32055,y:32896,ptovrint:False,ptlb:Burnout Map,ptin:_BurnoutMap,varname:node_5373,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2c29d28c1391c2a4890281876f5b39aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5741,x:32437,y:32947,varname:node_5741,prsc:2|A-4189-OUT,B-7497-OUT;n:type:ShaderForge.SFN_Slider,id:400,x:31938,y:33139,ptovrint:False,ptlb:Burnout Amount,ptin:_BurnoutAmount,varname:node_400,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2756,x:33113,y:32551,varname:node_2756,prsc:2|A-2500-RGB,B-5452-OUT;n:type:ShaderForge.SFN_Clamp01,id:5452,x:32652,y:32947,varname:node_5452,prsc:2|IN-5741-OUT;n:type:ShaderForge.SFN_Multiply,id:3276,x:32935,y:32687,varname:node_3276,prsc:2|A-9340-RGB,B-5913-OUT;n:type:ShaderForge.SFN_Color,id:9340,x:32736,y:32687,ptovrint:False,ptlb:Burnout Color,ptin:_BurnoutColor,varname:node_9340,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.53286,c3:0.2794118,c4:1;n:type:ShaderForge.SFN_OneMinus,id:3071,x:32847,y:32947,varname:node_3071,prsc:2|IN-5452-OUT;n:type:ShaderForge.SFN_Add,id:3407,x:33323,y:32687,varname:node_3407,prsc:2|A-2756-OUT,B-8233-OUT;n:type:ShaderForge.SFN_Multiply,id:8233,x:33113,y:32687,varname:node_8233,prsc:2|A-6259-OUT,B-3276-OUT;n:type:ShaderForge.SFN_Slider,id:6259,x:32579,y:32600,ptovrint:False,ptlb:Burnout Emission Power,ptin:_BurnoutEmissionPower,varname:node_6259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:10;n:type:ShaderForge.SFN_Slider,id:4104,x:32652,y:33138,ptovrint:False,ptlb:Burnout Color Expansion,ptin:_BurnoutColorExpansion,varname:node_4104,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1347521,max:1;n:type:ShaderForge.SFN_Subtract,id:970,x:33044,y:32947,varname:node_970,prsc:2|A-3071-OUT,B-4104-OUT;n:type:ShaderForge.SFN_Clamp01,id:5913,x:33226,y:32947,varname:node_5913,prsc:2|IN-970-OUT;n:type:ShaderForge.SFN_Tex2d,id:7601,x:33427,y:32518,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_7601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_RemapRange,id:4189,x:32232,y:32896,varname:node_4189,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1.5|IN-5373-R;n:type:ShaderForge.SFN_RemapRange,id:7497,x:32275,y:33139,varname:node_7497,prsc:2,frmn:0,frmx:1,tomn:0,tomx:20|IN-400-OUT;n:type:ShaderForge.SFN_Fresnel,id:5891,x:33687,y:33110,varname:node_5891,prsc:2|NRM-9687-OUT,EXP-2257-OUT;n:type:ShaderForge.SFN_Slider,id:2257,x:33345,y:33216,ptovrint:False,ptlb:Fresnel Glow Expansion,ptin:_FresnelGlowExpansion,varname:node_2257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:3;n:type:ShaderForge.SFN_NormalVector,id:9687,x:33502,y:33045,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:6976,x:33872,y:33210,varname:node_6976,prsc:2|A-5891-OUT,B-141-OUT,C-9340-RGB;n:type:ShaderForge.SFN_Slider,id:141,x:33345,y:33327,ptovrint:False,ptlb:Fresnel Glow Power,ptin:_FresnelGlowPower,varname:node_141,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2;n:type:ShaderForge.SFN_Add,id:9665,x:34065,y:32817,varname:node_9665,prsc:2|A-3407-OUT,B-8776-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:8776,x:34083,y:33084,ptovrint:False,ptlb:Fresnel Glow Enable,ptin:_FresnelGlowEnable,varname:node_8776,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1316-OUT,B-6976-OUT;n:type:ShaderForge.SFN_Vector1,id:1316,x:33872,y:33084,varname:node_1316,prsc:2,v1:0;proporder:2500-7601-5373-9340-400-6259-4104-8776-2257-141;pass:END;sub:END;*/

Shader "VFX/Burnout-Matter" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _BurnoutMap ("Burnout Map", 2D) = "white" {}
        _BurnoutColor ("Burnout Color", Color) = (1,0.53286,0.2794118,1)
        _BurnoutAmount ("Burnout Amount", Range(0, 1)) = 1
        _BurnoutEmissionPower ("Burnout Emission Power", Range(1, 10)) = 1
        _BurnoutColorExpansion ("Burnout Color Expansion", Range(0, 1)) = 0.1347521
        [MaterialToggle] _FresnelGlowEnable ("Fresnel Glow Enable", Float ) = 0
        _FresnelGlowExpansion ("Fresnel Glow Expansion", Range(0, 3)) = 3
        _FresnelGlowPower ("Fresnel Glow Power", Range(0, 2)) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _BurnoutMap; uniform float4 _BurnoutMap_ST;
            uniform float _BurnoutAmount;
            uniform float4 _BurnoutColor;
            uniform float _BurnoutEmissionPower;
            uniform float _BurnoutColorExpansion;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _FresnelGlowExpansion;
            uniform float _FresnelGlowPower;
            uniform fixed _FresnelGlowEnable;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _BurnoutMap_var = tex2D(_BurnoutMap,TRANSFORM_TEX(i.uv0, _BurnoutMap));
                float node_5741 = ((_BurnoutMap_var.r*1.5+0.0)*(_BurnoutAmount*20.0+0.0));
                clip(node_5741 - 0.5);
////// Lighting:
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float node_5452 = saturate(node_5741);
                float3 emissive = (((_Texture_var.rgb*node_5452)+(_BurnoutEmissionPower*(_BurnoutColor.rgb*saturate(((1.0 - node_5452)-_BurnoutColorExpansion)))))+lerp( 0.0, (pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelGlowExpansion)*_FresnelGlowPower*_BurnoutColor.rgb), _FresnelGlowEnable ));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _BurnoutMap; uniform float4 _BurnoutMap_ST;
            uniform float _BurnoutAmount;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _BurnoutMap_var = tex2D(_BurnoutMap,TRANSFORM_TEX(i.uv0, _BurnoutMap));
                float node_5741 = ((_BurnoutMap_var.r*1.5+0.0)*(_BurnoutAmount*20.0+0.0));
                clip(node_5741 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
