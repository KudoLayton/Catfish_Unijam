// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34409,y:32744,varname:node_4013,prsc:2|diff-6615-OUT,normal-7125-RGB,emission-667-OUT,alpha-7794-OUT,voffset-1707-OUT,tess-7320-OUT;n:type:ShaderForge.SFN_Tex2d,id:3380,x:32050,y:32372,ptovrint:False,ptlb:Diffuse Map,ptin:_DiffuseMap,varname:node_3380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fba1f6f6cfb469048adb3cce2508dfc6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7125,x:32978,y:32619,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_7125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:75f7c9c28e590a0448948f314cf5f73d,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Desaturate,id:3013,x:32281,y:32372,varname:node_3013,prsc:2|COL-3380-RGB;n:type:ShaderForge.SFN_Fresnel,id:6761,x:31868,y:32731,varname:node_6761,prsc:2|NRM-4608-OUT,EXP-3135-OUT;n:type:ShaderForge.SFN_NormalVector,id:4608,x:31508,y:32646,prsc:2,pt:True;n:type:ShaderForge.SFN_Slider,id:3135,x:31508,y:32834,ptovrint:False,ptlb:Fresnel Exp,ptin:_FresnelExp,varname:node_3135,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.4,cur:1.17094,max:5;n:type:ShaderForge.SFN_Multiply,id:101,x:32074,y:32802,varname:node_101,prsc:2|A-6761-OUT,B-4202-RGB;n:type:ShaderForge.SFN_Color,id:4202,x:31868,y:32918,ptovrint:False,ptlb:Emissive Color,ptin:_EmissiveColor,varname:node_4202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.6413792,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:7258,x:32384,y:32879,varname:node_7258,prsc:2|A-101-OUT,B-865-OUT;n:type:ShaderForge.SFN_Slider,id:865,x:32047,y:32980,ptovrint:False,ptlb:Fresnel Emissive Power,ptin:_FresnelEmissivePower,varname:node_865,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.157884,max:2;n:type:ShaderForge.SFN_Multiply,id:6615,x:32512,y:32372,varname:node_6615,prsc:2|A-3431-OUT,B-3013-OUT;n:type:ShaderForge.SFN_Slider,id:3431,x:32165,y:32159,ptovrint:False,ptlb:Diffuse Brightness,ptin:_DiffuseBrightness,varname:node_3431,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1.5;n:type:ShaderForge.SFN_Tex2d,id:1161,x:32411,y:33064,ptovrint:False,ptlb:Opacity Map,ptin:_OpacityMap,varname:node_1161,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2c29d28c1391c2a4890281876f5b39aa,ntxv:0,isnm:False|UVIN-888-UVOUT;n:type:ShaderForge.SFN_Desaturate,id:6376,x:32586,y:33064,varname:node_6376,prsc:2|COL-1161-RGB;n:type:ShaderForge.SFN_TexCoord,id:888,x:32204,y:33064,varname:node_888,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Subtract,id:8107,x:33112,y:33138,varname:node_8107,prsc:2|A-877-OUT,B-5265-OUT;n:type:ShaderForge.SFN_Slider,id:451,x:32604,y:33258,ptovrint:False,ptlb:Opacity Distortion,ptin:_OpacityDistortion,varname:node_451,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_OneMinus,id:49,x:33298,y:33138,varname:node_49,prsc:2|IN-8107-OUT;n:type:ShaderForge.SFN_Clamp01,id:8976,x:32845,y:32887,varname:node_8976,prsc:2|IN-7942-OUT;n:type:ShaderForge.SFN_Tex2d,id:9694,x:32074,y:32650,ptovrint:False,ptlb:Emissive Noise Map,ptin:_EmissiveNoiseMap,varname:node_9694,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:49961893b02d2b644ae1c883263442ab,ntxv:0,isnm:False|UVIN-663-OUT;n:type:ShaderForge.SFN_Multiply,id:5501,x:32318,y:32591,varname:node_5501,prsc:2|A-4338-OUT,B-9694-RGB;n:type:ShaderForge.SFN_Slider,id:4338,x:31930,y:32559,ptovrint:False,ptlb:Noise Map Emissive Power,ptin:_NoiseMapEmissivePower,varname:node_4338,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.099465,max:2;n:type:ShaderForge.SFN_Slider,id:8609,x:31109,y:32371,ptovrint:False,ptlb:Noise Map Speed X,ptin:_NoiseMapSpeedX,varname:node_8609,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1728748,max:1;n:type:ShaderForge.SFN_Slider,id:1365,x:31109,y:32474,ptovrint:False,ptlb:Noise Map Speed Y,ptin:_NoiseMapSpeedY,varname:node_1365,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.04378881,max:1;n:type:ShaderForge.SFN_Append,id:2056,x:31433,y:32422,varname:node_2056,prsc:2|A-8609-OUT,B-1365-OUT;n:type:ShaderForge.SFN_Multiply,id:7178,x:31604,y:32371,varname:node_7178,prsc:2|A-7354-T,B-2056-OUT;n:type:ShaderForge.SFN_Time,id:7354,x:31433,y:32293,varname:node_7354,prsc:2;n:type:ShaderForge.SFN_Add,id:663,x:31808,y:32322,varname:node_663,prsc:2|A-8098-UVOUT,B-7178-OUT;n:type:ShaderForge.SFN_TexCoord,id:8098,x:31604,y:32222,varname:node_8098,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8082,x:32512,y:32591,varname:node_8082,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:1.5|IN-5501-OUT;n:type:ShaderForge.SFN_Add,id:7942,x:32586,y:32879,varname:node_7942,prsc:2|A-2892-OUT,B-7258-OUT;n:type:ShaderForge.SFN_Multiply,id:2892,x:32749,y:32723,varname:node_2892,prsc:2|A-8082-OUT,B-4202-RGB;n:type:ShaderForge.SFN_Multiply,id:3386,x:33416,y:32931,varname:node_3386,prsc:2|A-8561-OUT,B-8976-OUT;n:type:ShaderForge.SFN_Slider,id:8561,x:33011,y:32812,ptovrint:False,ptlb:Emissive Power,ptin:_EmissivePower,varname:node_8561,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:877,x:32761,y:33064,varname:node_877,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:1.5|IN-6376-OUT;n:type:ShaderForge.SFN_Round,id:5871,x:33475,y:33138,varname:node_5871,prsc:2|IN-49-OUT;n:type:ShaderForge.SFN_Clamp01,id:5387,x:33656,y:33138,varname:node_5387,prsc:2|IN-5871-OUT;n:type:ShaderForge.SFN_Multiply,id:1707,x:33538,y:33371,varname:node_1707,prsc:2|A-9694-RGB,B-1927-OUT,C-6652-OUT;n:type:ShaderForge.SFN_NormalVector,id:1927,x:33177,y:33390,prsc:2,pt:True;n:type:ShaderForge.SFN_ValueProperty,id:7320,x:33791,y:33384,ptovrint:False,ptlb:Tesselation Value,ptin:_TesselationValue,varname:node_7320,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Slider,id:6652,x:33032,y:33563,ptovrint:False,ptlb:Vertex Offset Power,ptin:_VertexOffsetPower,varname:node_6652,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.04537974,max:0.1;n:type:ShaderForge.SFN_Clamp01,id:667,x:34113,y:32869,varname:node_667,prsc:2|IN-8423-OUT;n:type:ShaderForge.SFN_Add,id:13,x:33689,y:32787,varname:node_13,prsc:2|A-2356-OUT,B-3386-OUT;n:type:ShaderForge.SFN_Tex2d,id:239,x:32978,y:32438,ptovrint:False,ptlb:Emissive Map,ptin:_EmissiveMap,varname:node_239,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Desaturate,id:2874,x:33156,y:32438,varname:node_2874,prsc:2|COL-239-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:8423,x:33918,y:32869,ptovrint:False,ptlb:Emissive Map On,ptin:_EmissiveMapOn,varname:node_8423,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3386-OUT,B-13-OUT;n:type:ShaderForge.SFN_Multiply,id:2356,x:33491,y:32439,varname:node_2356,prsc:2|A-2874-OUT,B-2706-RGB,C-7217-OUT;n:type:ShaderForge.SFN_Color,id:2706,x:33156,y:32272,ptovrint:False,ptlb:Emissive Map Color,ptin:_EmissiveMapColor,varname:node_2706,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8482759,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:7217,x:33156,y:32597,ptovrint:False,ptlb:Emissive Map Power,ptin:_EmissiveMapPower,varname:node_7217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:7794,x:33896,y:33148,varname:node_7794,prsc:2|A-5387-OUT,B-1149-OUT;n:type:ShaderForge.SFN_Slider,id:1149,x:33531,y:33289,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_1149,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:5265,x:32956,y:33253,varname:node_5265,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-451-OUT;proporder:3380-3431-7125-1161-451-1149-9694-4202-8561-3135-865-4338-8609-1365-7320-6652-239-8423-2706-7217;pass:END;sub:END;*/

Shader "VFX/Special/Bones-Glowing" {
    Properties {
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _DiffuseBrightness ("Diffuse Brightness", Range(0, 1.5)) = 0
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _OpacityMap ("Opacity Map", 2D) = "white" {}
        _OpacityDistortion ("Opacity Distortion", Range(0, 1)) = 1
        _Opacity ("Opacity", Range(0, 1)) = 1
        _EmissiveNoiseMap ("Emissive Noise Map", 2D) = "white" {}
        _EmissiveColor ("Emissive Color", Color) = (1,0.6413792,0,1)
        _EmissivePower ("Emissive Power", Range(0, 1)) = 1
        _FresnelExp ("Fresnel Exp", Range(0.4, 5)) = 1.17094
        _FresnelEmissivePower ("Fresnel Emissive Power", Range(0, 2)) = 1.157884
        _NoiseMapEmissivePower ("Noise Map Emissive Power", Range(0, 2)) = 1.099465
        _NoiseMapSpeedX ("Noise Map Speed X", Range(0, 1)) = 0.1728748
        _NoiseMapSpeedY ("Noise Map Speed Y", Range(0, 1)) = 0.04378881
        _TesselationValue ("Tesselation Value", Float ) = 3
        _VertexOffsetPower ("Vertex Offset Power", Range(0, 0.1)) = 0.04537974
        _EmissiveMap ("Emissive Map", 2D) = "white" {}
        [MaterialToggle] _EmissiveMapOn ("Emissive Map On", Float ) = 0
        _EmissiveMapColor ("Emissive Map Color", Color) = (1,0.8482759,0,1)
        _EmissiveMapPower ("Emissive Map Power", Range(0, 2)) = 1
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _FresnelExp;
            uniform float4 _EmissiveColor;
            uniform float _FresnelEmissivePower;
            uniform float _DiffuseBrightness;
            uniform sampler2D _OpacityMap; uniform float4 _OpacityMap_ST;
            uniform float _OpacityDistortion;
            uniform sampler2D _EmissiveNoiseMap; uniform float4 _EmissiveNoiseMap_ST;
            uniform float _NoiseMapEmissivePower;
            uniform float _NoiseMapSpeedX;
            uniform float _NoiseMapSpeedY;
            uniform float _EmissivePower;
            uniform float _TesselationValue;
            uniform float _VertexOffsetPower;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform fixed _EmissiveMapOn;
            uniform float4 _EmissiveMapColor;
            uniform float _EmissiveMapPower;
            uniform float _Opacity;
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
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_7354 = _Time;
                float2 node_663 = (o.uv0+(node_7354.g*float2(_NoiseMapSpeedX,_NoiseMapSpeedY)));
                float4 _EmissiveNoiseMap_var = tex2Dlod(_EmissiveNoiseMap,float4(TRANSFORM_TEX(node_663, _EmissiveNoiseMap),0.0,0));
                v.vertex.xyz += (_EmissiveNoiseMap_var.rgb*v.normal*_VertexOffsetPower);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselationValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(i.uv0, _DiffuseMap));
                float node_6615 = (_DiffuseBrightness*dot(_DiffuseMap_var.rgb,float3(0.3,0.59,0.11)));
                float3 diffuseColor = float3(node_6615,node_6615,node_6615);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_7354 = _Time;
                float2 node_663 = (i.uv0+(node_7354.g*float2(_NoiseMapSpeedX,_NoiseMapSpeedY)));
                float4 _EmissiveNoiseMap_var = tex2D(_EmissiveNoiseMap,TRANSFORM_TEX(node_663, _EmissiveNoiseMap));
                float3 node_3386 = (_EmissivePower*saturate(((((_NoiseMapEmissivePower*_EmissiveNoiseMap_var.rgb)*2.1+-0.6)*_EmissiveColor.rgb)+((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)*_EmissiveColor.rgb)*_FresnelEmissivePower))));
                float4 _EmissiveMap_var = tex2D(_EmissiveMap,TRANSFORM_TEX(i.uv0, _EmissiveMap));
                float3 emissive = saturate(lerp( node_3386, ((dot(_EmissiveMap_var.rgb,float3(0.3,0.59,0.11))*_EmissiveMapColor.rgb*_EmissiveMapPower)+node_3386), _EmissiveMapOn ));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _OpacityMap_var = tex2D(_OpacityMap,TRANSFORM_TEX(i.uv0, _OpacityMap));
                fixed4 finalRGBA = fixed4(finalColor,(saturate(round((1.0 - ((dot(_OpacityMap_var.rgb,float3(0.3,0.59,0.11))*2.0+-0.5)-(_OpacityDistortion*2.0+-1.0)))))*_Opacity));
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _FresnelExp;
            uniform float4 _EmissiveColor;
            uniform float _FresnelEmissivePower;
            uniform float _DiffuseBrightness;
            uniform sampler2D _OpacityMap; uniform float4 _OpacityMap_ST;
            uniform float _OpacityDistortion;
            uniform sampler2D _EmissiveNoiseMap; uniform float4 _EmissiveNoiseMap_ST;
            uniform float _NoiseMapEmissivePower;
            uniform float _NoiseMapSpeedX;
            uniform float _NoiseMapSpeedY;
            uniform float _EmissivePower;
            uniform float _TesselationValue;
            uniform float _VertexOffsetPower;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform fixed _EmissiveMapOn;
            uniform float4 _EmissiveMapColor;
            uniform float _EmissiveMapPower;
            uniform float _Opacity;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_7354 = _Time;
                float2 node_663 = (o.uv0+(node_7354.g*float2(_NoiseMapSpeedX,_NoiseMapSpeedY)));
                float4 _EmissiveNoiseMap_var = tex2Dlod(_EmissiveNoiseMap,float4(TRANSFORM_TEX(node_663, _EmissiveNoiseMap),0.0,0));
                v.vertex.xyz += (_EmissiveNoiseMap_var.rgb*v.normal*_VertexOffsetPower);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselationValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(i.uv0, _DiffuseMap));
                float node_6615 = (_DiffuseBrightness*dot(_DiffuseMap_var.rgb,float3(0.3,0.59,0.11)));
                float3 diffuseColor = float3(node_6615,node_6615,node_6615);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _OpacityMap_var = tex2D(_OpacityMap,TRANSFORM_TEX(i.uv0, _OpacityMap));
                fixed4 finalRGBA = fixed4(finalColor * (saturate(round((1.0 - ((dot(_OpacityMap_var.rgb,float3(0.3,0.59,0.11))*2.0+-0.5)-(_OpacityDistortion*2.0+-1.0)))))*_Opacity),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _EmissiveNoiseMap; uniform float4 _EmissiveNoiseMap_ST;
            uniform float _NoiseMapSpeedX;
            uniform float _NoiseMapSpeedY;
            uniform float _TesselationValue;
            uniform float _VertexOffsetPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_7354 = _Time;
                float2 node_663 = (o.uv0+(node_7354.g*float2(_NoiseMapSpeedX,_NoiseMapSpeedY)));
                float4 _EmissiveNoiseMap_var = tex2Dlod(_EmissiveNoiseMap,float4(TRANSFORM_TEX(node_663, _EmissiveNoiseMap),0.0,0));
                v.vertex.xyz += (_EmissiveNoiseMap_var.rgb*v.normal*_VertexOffsetPower);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselationValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
