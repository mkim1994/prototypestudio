Shader "MY_TEXT/MIX_COLOR_FONT"
{
	Properties
	{
        _MainTex ("Font Texture", 2D) = "white" {} 
        _Color ("Text Color", Color) = (1,1,1,1) 

	}
	SubShader
	{
        Tags {"Glowable"="True" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" } 
        Lighting Off Cull Off ZWrite Off Fog { Mode Off } 
        Blend SrcAlpha OneMinusSrcAlpha 

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color: COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : float4;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				o.color = _Color * v.color;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				col.rgb = i.color.rgb;
				col.a *= _Color.a;
				return col;
			}
			ENDCG
		}
	}
}
