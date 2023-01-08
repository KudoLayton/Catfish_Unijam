// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34128,y:32784,varname:node_4013,prsc:2|diff-8893-OUT,emission-9064-OUT,alpha-5093-OUT;n:type:ShaderForge.SFN_Tex2d,id:9825,x:32076,y:32531,ptovrint:False,ptlb:Duffuse Map,ptin:_DuffuseMap,varname:node_9825,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d67016f8a4622724c91d1a2dff72511e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Desaturate,id:5634,x:32251,y:32531,varname:node_5634,prsc:2|COL-9825-RGB;n:type:ShaderForge.SFN_Tex2d,id:9044,x:32076,y:32850,ptovrint:False,ptlb:Emissive Map,ptin:_EmissiveMap,varname:node_9044,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:658275fdeab52a3449fa4b5623a734f5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Desaturate,id:2634,x:32248,y:32850,varname:node_2634,prsc:2|COL-9044-RGB;n:type:ShaderForge.SFN_Color,id:872,x:31564,y:32055,ptovrint:False,ptlb:Emissive Color,ptin:_EmissiveColor,varname:node_872,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:8893,x:32471,y:32531,varname:node_8893,prsc:2|A-6106-OUT,B-5634-OUT,C-527-OUT;n:type:ShaderForge.SFN_Slider,id:527,x:32076,y:32721,ptovrint:False,ptlb:Diffuse Map Power,ptin:_DiffuseMapPower,varname:node_527,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.5,cur:0.9957216,max:1.5;n:type:ShaderForge.SFN_Multiply,id:9064,x:32936,y:32973,varname:node_9064,prsc:2|A-7531-OUT,B-6106-OUT;n:type:ShaderForge.SFN_Slider,id:9234,x:32182,y:33186,ptovrint:False,ptlb:Emissive Power,ptin:_EmissivePower,varname:node_9234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.285103,max:3;n:type:ShaderForge.SFN_Fresnel,id:9913,x:31923,y:33297,varname:node_9913,prsc:2|NRM-2304-OUT,EXP-6505-OUT;n:type:ShaderForge.SFN_NormalVector,id:2304,x:31696,y:33159,prsc:2,pt:True;n:type:ShaderForge.SFN_Slider,id:6505,x:31561,y:33359,ptovrint:False,ptlb:Fresnel Exp,ptin:_FresnelExp,varname:node_6505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.3,cur:3,max:3;n:type:ShaderForge.SFN_Add,id:9853,x:32488,y:32876,varname:node_9853,prsc:2|A-2634-OUT,B-9918-OUT;n:type:ShaderForge.SFN_Multiply,id:9918,x:32116,y:33297,varname:node_9918,prsc:2|A-9913-OUT,B-2201-OUT;n:type:ShaderForge.SFN_Slider,id:2201,x:31766,y:33471,ptovrint:False,ptlb:Fresnel Power,ptin:_FresnelPower,varname:node_2201,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7968682,max:2;n:type:ShaderForge.SFN_Tex2d,id:8752,x:31457,y:32880,ptovrint:False,ptlb:Emissive Heat Map,ptin:_EmissiveHeatMap,varname:node_8752,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:49961893b02d2b644ae1c883263442ab,ntxv:0,isnm:False|UVIN-1733-OUT;n:type:ShaderForge.SFN_OneMinus,id:1545,x:31624,y:32880,varname:node_1545,prsc:2|IN-8752-RGB;n:type:ShaderForge.SFN_RemapRange,id:3907,x:31786,y:32880,varname:node_3907,prsc:2,frmn:0,frmx:1,tomn:-3,tomx:1|IN-1545-OUT;n:type:ShaderForge.SFN_Slider,id:9041,x:30664,y:32982,ptovrint:False,ptlb:Emissive Heat Speed X,ptin:_EmissiveHeatSpeedX,varname:node_9041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:-0.03134337,max:0.5;n:type:ShaderForge.SFN_Slider,id:3224,x:30664,y:33070,ptovrint:False,ptlb:Emissive Heat Speed Y,ptin:_EmissiveHeatSpeedY,varname:node_3224,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0.03492202,max:0.5;n:type:ShaderForge.SFN_Append,id:577,x:30986,y:33013,varname:node_577,prsc:2|A-9041-OUT,B-3224-OUT;n:type:ShaderForge.SFN_Multiply,id:5602,x:31139,y:32930,varname:node_5602,prsc:2|A-4664-T,B-577-OUT;n:type:ShaderForge.SFN_Time,id:4664,x:30974,y:32870,varname:node_4664,prsc:2;n:type:ShaderForge.SFN_Add,id:1733,x:31288,y:32847,varname:node_1733,prsc:2|A-7038-UVOUT,B-5602-OUT;n:type:ShaderForge.SFN_TexCoord,id:7038,x:31139,y:32759,varname:node_7038,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Clamp01,id:7138,x:31973,y:33010,varname:node_7138,prsc:2|IN-4908-OUT;n:type:ShaderForge.SFN_Multiply,id:2802,x:32681,y:32876,varname:node_2802,prsc:2|A-9853-OUT,B-9234-OUT;n:type:ShaderForge.SFN_Add,id:7531,x:32733,y:33014,varname:node_7531,prsc:2|A-2802-OUT,B-7138-OUT;n:type:ShaderForge.SFN_Multiply,id:4908,x:31915,y:32742,varname:node_4908,prsc:2|A-700-OUT,B-3907-OUT;n:type:ShaderForge.SFN_Slider,id:700,x:31592,y:32693,ptovrint:False,ptlb:Emissive Heat Power,ptin:_EmissiveHeatPower,varname:node_700,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.331698,max:2;n:type:ShaderForge.SFN_Exp,id:7666,x:31764,y:31998,varname:node_7666,prsc:2,et:1|IN-872-RGB;n:type:ShaderForge.SFN_Multiply,id:1598,x:31952,y:32026,varname:node_1598,prsc:2|A-8567-OUT,B-7666-OUT;n:type:ShaderForge.SFN_Slider,id:8567,x:31549,y:31862,ptovrint:False,ptlb:Emissive Color Exp Power,ptin:_EmissiveColorExpPower,varname:node_8567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_SwitchProperty,id:6106,x:32155,y:32227,ptovrint:False,ptlb:Emissive Color Exp,ptin:_EmissiveColorExp,varname:node_6106,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-872-RGB,B-1598-OUT;n:type:ShaderForge.SFN_Tex2d,id:208,x:32617,y:33162,ptovrint:False,ptlb:Opacity Subtract Map,ptin:_OpacitySubtractMap,varname:node_208,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2c29d28c1391c2a4890281876f5b39aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1656,x:32505,y:33402,ptovrint:False,ptlb:Opacity Subtract,ptin:_OpacitySubtract,varname:node_1656,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Clamp01,id:9885,x:33432,y:33137,varname:node_9885,prsc:2|IN-7785-OUT;n:type:ShaderForge.SFN_Multiply,id:5093,x:33788,y:33137,varname:node_5093,prsc:2|A-8345-OUT,B-1288-OUT;n:type:ShaderForge.SFN_Slider,id:9562,x:33354,y:33347,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9562,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:1288,x:33685,y:33347,varname:node_1288,prsc:2|IN-9562-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:7785,x:33256,y:33137,varname:node_7785,prsc:2|IN-7797-OUT,IMIN-6633-OUT,IMAX-6544-OUT,OMIN-2261-OUT,OMAX-6544-OUT;n:type:ShaderForge.SFN_Vector1,id:6633,x:33023,y:33250,varname:node_6633,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:6544,x:33023,y:33191,varname:node_6544,prsc:2,v1:1;n:type:ShaderForge.SFN_Round,id:8345,x:33612,y:33137,varname:node_8345,prsc:2|IN-9885-OUT;n:type:ShaderForge.SFN_RemapRange,id:7797,x:32835,y:33162,varname:node_7797,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-208-R;n:type:ShaderForge.SFN_RemapRange,id:2261,x:33006,y:33402,varname:node_2261,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1270-OUT;n:type:ShaderForge.SFN_OneMinus,id:1270,x:32835,y:33402,varname:node_1270,prsc:2|IN-1656-OUT;proporder:9825-527-9044-872-6106-8567-9234-6505-2201-8752-700-9041-3224-208-1656-9562;pass:END;sub:END;*/

Shader "VFX/Special/Crystal-Glowing" {
    Properties {
        _DuffuseMap ("Duffuse Map", 2D) = "white" {}
        _DiffuseMapPower ("Diffuse Map Power", Range(0.5, 1.5)) = 0.9957216
        _EmissiveMap ("Emissive Map", 2D) = "white" {}
        _EmissiveColor ("Emissive Color", Color) = (1,1,1,1)
        [MaterialToggle] _EmissiveColorExp ("Emissive Color Exp", Float ) = 1
        _EmissiveColorExpPower ("Emissive Color Exp Power", Range(0, 1)) = 1
        _EmissivePower ("Emissive Power", Range(0, 3)) = 1.285103
        _FresnelExp ("Fresnel Exp", Range(0.3, 3)) = 3
        _FresnelPower ("Fresnel Power", Range(0, 2)) = 0.7968682
        _EmissiveHeatMap ("Emissive Heat Map", 2D) = "white" {}
        _EmissiveHeatPower ("Emissive Heat Power", Range(0, 2)) = 1.331698
        _EmissiveHeatSpeedX ("Emissive Heat Speed X", Range(-0.5, 0.5)) = -0.03134337
        _EmissiveHeatSpeedY ("Emissive Heat Speed Y", Range(-0.5, 0.5)) = 0.03492202
        _OpacitySubtractMap ("Opacity Subtract Map", 2D) = "white" {}
        _OpacitySubtract ("Opacity Subtract", Range(0, 1)) = 0
        _Opacity ("Opacity", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DuffuseMap; uniform float4 _DuffuseMap_ST;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform float4 _EmissiveColor;
            uniform float _DiffuseMapPower;
            uniform float _EmissivePower;
            uniform float _FresnelExp;
            uniform float _FresnelPower;
            uniform sampler2D _EmissiveHeatMap; uniform float4 _EmissiveHeatMap_ST;
            uniform float _EmissiveHeatSpeedX;
            uniform float _EmissiveHeatSpeedY;
            uniform float _EmissiveHeatPower;
            uniform float _EmissiveColorExpPower;
            uniform fixed _EmissiveColorExp;
            uniform sampler2D _OpacitySubtractMap; uniform float4 _OpacitySubtractMap_ST;
            uniform float _OpacitySubtract;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 _EmissiveColorExp_var = lerp( _EmissiveColor.rgb, (_EmissiveColorExpPower*exp2(_EmissiveColor.rgb)), _EmissiveColorExp );
                float4 _DuffuseMap_var = tex2D(_DuffuseMap,TRANSFORM_TEX(i.uv0, _DuffuseMap));
                float3 diffuseColor = (_EmissiveColorExp_var*dot(_DuffuseMap_var.rgb,float3(0.3,0.59,0.11))*_DiffuseMapPower);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissiveMap_var = tex2D(_EmissiveMap,TRANSFORM_TEX(i.uv0, _EmissiveMap));
                float4 node_4664 = _Time;
                float2 node_1733 = (i.uv0+(node_4664.g*float2(_EmissiveHeatSpeedX,_EmissiveHeatSpeedY)));
                float4 _EmissiveHeatMap_var = tex2D(_EmissiveHeatMap,TRANSFORM_TEX(node_1733, _EmissiveHeatMap));
                float3 emissive = ((((dot(_EmissiveMap_var.rgb,float3(0.3,0.59,0.11))+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)*_FresnelPower))*_EmissivePower)+saturate((_EmissiveHeatPower*((1.0 - _EmissiveHeatMap_var.rgb)*4.0+-3.0))))*_EmissiveColorExp_var);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _OpacitySubtractMap_var = tex2D(_OpacitySubtractMap,TRANSFORM_TEX(i.uv0, _OpacitySubtractMap));
                float node_6633 = 0.0;
                float node_6544 = 1.0;
                float node_2261 = ((1.0 - _OpacitySubtract)*2.0+-1.0);
                fixed4 finalRGBA = fixed4(finalColor,(round(saturate((node_2261 + ( ((_OpacitySubtractMap_var.r*2.0+-1.0) - node_6633) * (node_6544 - node_2261) ) / (node_6544 - node_6633))))*(1.0 - _Opacity)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DuffuseMap; uniform float4 _DuffuseMap_ST;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform float4 _EmissiveColor;
            uniform float _DiffuseMapPower;
            uniform float _EmissivePower;
            uniform float _FresnelExp;
            uniform float _FresnelPower;
            uniform sampler2D _EmissiveHeatMap; uniform float4 _EmissiveHeatMap_ST;
            uniform float _EmissiveHeatSpeedX;
            uniform float _EmissiveHeatSpeedY;
            uniform float _EmissiveHeatPower;
            uniform float _EmissiveColorExpPower;
            uniform fixed _EmissiveColorExp;
            uniform sampler2D _OpacitySubtractMap; uniform float4 _OpacitySubtractMap_ST;
            uniform float _OpacitySubtract;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 _EmissiveColorExp_var = lerp( _EmissiveColor.rgb, (_EmissiveColorExpPower*exp2(_EmissiveColor.rgb)), _EmissiveColorExp );
                float4 _DuffuseMap_var = tex2D(_DuffuseMap,TRANSFORM_TEX(i.uv0, _DuffuseMap));
                float3 diffuseColor = (_EmissiveColorExp_var*dot(_DuffuseMap_var.rgb,float3(0.3,0.59,0.11))*_DiffuseMapPower);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _OpacitySubtractMap_var = tex2D(_OpacitySubtractMap,TRANSFORM_TEX(i.uv0, _OpacitySubtractMap));
                float node_6633 = 0.0;
                float node_6544 = 1.0;
                float node_2261 = ((1.0 - _OpacitySubtract)*2.0+-1.0);
                fixed4 finalRGBA = fixed4(finalColor * (round(saturate((node_2261 + ( ((_OpacitySubtractMap_var.r*2.0+-1.0) - node_6633) * (node_6544 - node_2261) ) / (node_6544 - node_6633))))*(1.0 - _Opacity)),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
