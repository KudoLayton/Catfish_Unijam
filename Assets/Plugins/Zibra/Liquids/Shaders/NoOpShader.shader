Shader "ZibraLiquids/NoOpShader"
{
    SubShader
    {
        Pass
        {
            Cull Off
            ZWrite Off
            ZTest Always

            HLSLPROGRAM
            #pragma instancing_options procedural:setup
            #pragma vertex VSMain
            #pragma fragment PSMain
            #pragma target 3.5

            struct VSIn
            {
                uint vertexID : SV_VertexID;
            };

            struct VSOut
            {
                float4 position : POSITION;
            };

            struct PSOut
            {
                float4 color : COLOR;
            };

            VSOut VSMain(VSIn input)
            {
                VSOut output;
                output.position = float4(-1.0f, -1.0f, -1.0f, 1.0);

                return output;
            }

            PSOut PSMain(VSOut input)
            {
                PSOut output;
                output.color = 0.0f;

                return output;
            }
            
            ENDHLSL
        }
    }
}
