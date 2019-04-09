// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

////////////////////////////////////////////
// CameraFilterPack - by VETASOFT 2017 /////
////////////////////////////////////////////

Shader "CameraFilterPack/FX_Psycho" {
    HLSLINCLUDE

        #pragma target 3.0
        #pragma fragment frag
        #pragma fragmentoption ARB_precision_hint_fastest
        #pragma target 3.0

        #include "PostProcessing/Shaders/StdLib.hlsl"
        #include "PostProcessing/Shaders/Colors.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);

        Properties {
          _MainTex ("Base (RGB)", 2D) = "white" {}
          _TimeX ("Time", Range(0.0, 1.0)) = 1.0
          _Distortion ("_Distortion", Range(0.0, 1.0)) = 1.0
        }

        float _Blend;
        float4 _MainTex_TexelSize;
        float4 _Params;
        uniform sampler2D _MainTex;
        uniform float _TimeX;
        uniform float _Distortion;

                
        inline float4 mod(float4 x,float4 modu) {
          return x - floor(x * (1.0 / modu)) * modu;
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
          float2 xy  = i.texcoord;
          float4 tx = tex2D(_MainTex, xy);
          float4 x=tx;
          x = mod(x + .5*.08, 5.)* 0.1 *_TimeX;
          x = floor(x*30.)*_Distortion;
          x = mod(x,2.);
          return float4(x.rgb, tx.a);
        }

        struct appdata_t {
          float4 vertex   : POSITION;
          float4 color    : COLOR;
          float2 texcoord : TEXCOORD0;
        };


    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

         Pass
        {
            HLSLPROGRAM

                #pragma vertex Vert
                #pragma fragment Frag

            ENDHLSL
        }
    }
  }

