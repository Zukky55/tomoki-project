﻿Shader "CameraFade"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader
		{
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;

					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2f vert(appdata v)
				{
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
				fixed4 _Color;

				fixed4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
					return UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv) * _Color;
				}
				ENDCG
			}
		}
}