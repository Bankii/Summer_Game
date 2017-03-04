Shader "Custom/TransitionTest" 
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Magnitude("Hello", Range(0,1)) = 1
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _Magnitude;
			fixed4 _Color;

			fixed4 simplefrag(v2f i) : SV_Target
			{
				if (i.uv.x < _Magnitude)
					return _Color;
				return tex2D(_MainTex, i.uv);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = simplefrag(i);
				return col;
			}
			ENDCG
		}
	}
}

