Shader "HD/Lightening"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_ZigZagMag("Zig Zag", Float) = .1
		_SinMag("Sin", Float) = .1
		_Scale("Scale (width)", Float) = .1
		_XScale("XScale (length)", Float) = .1
		_RngSeed("Seed", Float) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
#pragma multi_compile_instancing

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
			float4 _MainTex_ST;
			float4 _Color;
			float _ZigZagMag;
			float _SinMag;

			UNITY_INSTANCING_CBUFFER_START(Props)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _RngSeed)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _Scale)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _XScale)
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
				// x is .62 -> -.62

				/*if (v.vertex.x < -.62) {
					v.vertex.y += 10;
				}
*/
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v); // Must be called before ACCESS_INSTANCED
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 test = v.vertex;

				float xPercent = test.x / .62; // -1 -> 1
				xPercent += 1.0;
				xPercent /= 2.0;
				xPercent = max(0, min(1, xPercent));
				// xPercent 0->1

				float delta = _Time.z* .5;//% 1.0; // *  rng(.05, .15, 128))
				// delta 0->1 repeating

				float xPercent2 = xPercent ;
				xPercent2 %= 1.0; 

				// xPercent2 0->1 repeating and smoothed 

				/*xPercent2 += breakStartPercent;
				xPercent2 %= 1.0;*/


				float numberOfBreaks = 5.0 * UNITY_ACCESS_INSTANCED_PROP(_XScale);
				int breakNumber = xPercent2 * numberOfBreaks;

				float breakStartPercent = breakNumber / numberOfBreaks + delta;
				breakStartPercent %= 1.0;

				float lengthOfBreak = 1.0 / numberOfBreaks;
				float distanceIntoBreak = xPercent2 - breakStartPercent;
				float percentIntoBreak = distanceIntoBreak / lengthOfBreak;


				v.vertex.y *= 1.0-xPercent2;


				float percentMag = (1.0 - abs((xPercent2 * 2.0 - 1.0)));

				v.vertex.y += .3 * (percentIntoBreak * _ZigZagMag * percentMag) / UNITY_ACCESS_INSTANCED_PROP(_Scale);


				
				float sinPosition = sin(xPercent + _Time.z * rng(1, 10, 817.6));
				v.vertex.y += (sinPosition * _SinMag * rng(.5, 1.5, 41.99) * percentMag * UNITY_ACCESS_INSTANCED_PROP(_XScale)) / UNITY_ACCESS_INSTANCED_PROP(_Scale);

							
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
			return col * _Color;
		}
		ENDCG
	}
	}
}
