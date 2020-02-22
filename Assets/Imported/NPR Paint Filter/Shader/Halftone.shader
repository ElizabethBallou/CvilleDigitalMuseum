Shader "NPR Paint Filter/Halftone" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			fixed4 _Color1, _Color2;
			float _Lightness, _Density, _SmoothEdge, _Radius, _OrigFactor, _HalfToneFactor;

			float4 frag (v2f_img input) : SV_Target
			{
				fixed4 tc = tex2D(_MainTex, input.uv);
				float lightness = (tc.r * 0.3 + tc.g * 0.6 + tc.b * 0.1) * _Lightness;
				float radius = 1 - lightness + _Radius;

				float aspect = _ScreenParams.x / _ScreenParams.y;
				float2 uv = input.uv;
				uv.x *= aspect;
				float2 uv2 = mul(float2x2(0.707, -0.707, 0.707, 0.707), uv);
				fixed2 vc = 2 * frac(_Density * uv2) - 1;
				float distance = length(vc);
				fixed4 halftoneColor = lerp(_Color1, _Color2, smoothstep(radius, radius +_SmoothEdge, distance));
				fixed4 blendColor = tc * _OrigFactor + tc * halftoneColor * _HalfToneFactor;
				return lerp(blendColor, halftoneColor, _HalfToneFactor);
			}
			ENDCG
		}
	}
	FallBack Off
}