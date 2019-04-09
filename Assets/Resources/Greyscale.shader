Shader "Hidden/Custom/Grayscale"
{
    HLSLINCLUDE

        #pragma target 3.0

        #include "PostProcessing/Shaders/StdLib.hlsl"
        #include "PostProcessing/Shaders/Colors.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
        float _Blend;
        float4 _MainTex_TexelSize;
        float4 _Params;
        float4 Frag(VaryingsDefault i) : SV_Target
        {
          float d = SAMPLE_DEPTH_TEXTURE_LOD(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo, 0);
          d = lerp(d, LinearEyeDepth(d), _Blend);

        //#if !UNITY_COLORSPACE_GAMMA
        //    d = SRGBToLinear(d);
        //#endif

          return float4(d.xxx, 5.0);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

         Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}
