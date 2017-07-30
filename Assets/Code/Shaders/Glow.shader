Shader "HD/Glow"
{
	Properties
	{

		_MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)
		_GlowColor("Glow color", Color) = (1,1,1,1)
			_Size("Size", Float) = 1
			_RngSeed("Seed", Float) = 1
		_Stretch("Stretch", Float) = 1
		_Progress("Progress", Float) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
		LOD 100
		Pass
	{
		CGPROGRAM
#pragma vertex vert alpha
#pragma fragment frag
#pragma multi_compile_instancing
		// make fog work
#pragma multi_compile_fog

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
		UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	sampler2D _MainTex;
	float4 _Color;
	float4 _GlowColor;
	float4 _MainTex_ST;
	float _Stretch;

	UNITY_INSTANCING_CBUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed, _RngSeed)
		UNITY_DEFINE_INSTANCED_PROP(fixed, _Size)
		UNITY_DEFINE_INSTANCED_PROP(fixed, _Progress)
	UNITY_INSTANCING_CBUFFER_END

	float rng(float usecaseSeed)
	{
		//float seed = sin(_RngSeed * 3.19) + sin(usecaseSeed * 3.410);
		////return frac(sin(objectSeed * 3.19) + sin(usecaseSeed * 3.410));
		//return frac(sin(seed) * 43758.5453);

		float objectSeed = UNITY_ACCESS_INSTANCED_PROP(_RngSeed);

		return fmod(frac(sin(objectSeed * 3.19) + sin(usecaseSeed * 3.410)) + 1.0, .5) + .5;
	}

	float rng(float min, float max, float usecaseSeed)
	{
		float random = rng(usecaseSeed);
		random *= max - min;
		random += min;
		return random;
	}

	v2f vert(appdata v)
	{
		v2f o;

		UNITY_SETUP_INSTANCE_ID(v); // Must be called before ACCESS_INSTANCED
		UNITY_TRANSFER_INSTANCE_ID(v, o);

		float t = (1 + abs(v.vertex.x)) * (1 + abs(v.vertex.y));

		// need 0->1->0->-1->...
		// mod 2 gives 0 -> 2
		// -1 = -1 -> 1
		// abs = 1 -> 0 -> 1
		// 1-that

		for (int i = 0; i < 3; i++)
		{
			//float2 randPos = normalize(float2(1 - abs((rng(-1, 1, 19.1) + _Time.z) % 2 - 1), 1 - abs((rng(-1, 1, 88.9) + _Time.z) % 2 - 1)));
			float2 randPos = normalize(float2(sin(1 - (rng(1, 3, (i + 9.8) * 19.1)* _Time.z)), sin(1 - (rng(1, 3, (i + 8.1) *88.9) * _Time.z))));


			//randPos *= 2;

			//randPos = float2(1, 1);
			float distance = length(normalize(v.vertex.xz) - randPos);
			if (distance < 1) {
				//v.vertex.x += 10;
				v.vertex.xz *= 1 + (1 - distance) * _Stretch *  UNITY_ACCESS_INSTANCED_PROP(_Size);
			}
		}

		v.vertex.x *= 1 + UNITY_ACCESS_INSTANCED_PROP(_Size) * rng(.5, 1.5, UNITY_ACCESS_INSTANCED_PROP(_Progress) * 129.81 * t);
		v.vertex.z *= 1 + UNITY_ACCESS_INSTANCED_PROP(_Size) * rng(.5, 1.5, UNITY_ACCESS_INSTANCED_PROP(_Progress) * 41.99 * t);
		v.vertex.y += 1;


		v.vertex.xz *= .75 + abs(sin(UNITY_ACCESS_INSTANCED_PROP(_Progress)))
			* .5 * min(1,UNITY_ACCESS_INSTANCED_PROP(_Size));

		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);
	return col * _GlowColor * float4(1,1,1,.5 * min(1, UNITY_ACCESS_INSTANCED_PROP(_Size))* min(1, UNITY_ACCESS_INSTANCED_PROP(_Size)));
}
	ENDCG
}
//	Pass
//	{
//		CGPROGRAM
//		#pragma vertex vert
//		#pragma fragment frag
//		// make fog work
//		#pragma multi_compile_fog
//
//		#include "UnityCG.cginc"
//
//		struct appdata
//		{
//			float4 vertex : POSITION;
//			float2 uv : TEXCOORD0;
//		};
//
//		struct v2f
//		{
//			float2 uv : TEXCOORD0;
//			UNITY_FOG_COORDS(1)
//			float4 vertex : SV_POSITION;
//		};
//
//		sampler2D _MainTex;
//		float4 _Color;
//		float4 _MainTex_ST;
//
//		v2f vert(appdata v)
//		{
//			v2f o;
//			o.vertex = UnityObjectToClipPos(v.vertex);
//			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//			UNITY_TRANSFER_FOG(o,o.vertex);
//			return o;
//		}
//
//		fixed4 frag(v2f i) : SV_Target
//		{
//			// sample the texture
//			fixed4 col = tex2D(_MainTex, i.uv);
//		// apply fog
//		UNITY_APPLY_FOG(i.fogCoord, col);
//		return col * _Color;
//	}
//	ENDCG
//}


	}
}
