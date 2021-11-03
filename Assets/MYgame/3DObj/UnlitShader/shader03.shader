﻿Shader "Unlit/shader03"
{
    Properties
    {

		_ColorMask("OutColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define TAU 6.28318530718

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex   : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 worldCoords : TEXCOORD1;
            };

            float4 _MainTex_ST;
			float4 _ColorMask;

            sampler2D _Pattern;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.worldCoords = mul(UNITY_MATRIX_M, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
    
                return o;
            }


            float4 frag (Interpolators i) : SV_Target
            {
				clip(-1.0);
                return _ColorMask;
            }
            ENDCG
        }
    }
}
