// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'

Shader "Projector/ProjectAlpha"
{
	Properties
	{
		_ShadowTex ("Cookie", 2D) = "gray" {}
	}
	Subshader
	{
		Tags { "Queue"="Transparent+1"}
		Pass
		{
			ZWrite Off
			Blend Zero One, One One
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
		   struct Input
		   {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			//float4 color : COLOR;
		   };

			struct v2f
			{
				float4 uvShadow : TEXCOORD0;
//				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
				//float4 color : COLOR0;
				fixed nv : COLOR0;
			};
			
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			
			v2f vert (Input v)
			{
//				v2f o;
//				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
//				o.uvShadow = mul (unity_Projector, v.vertex);
//				UNITY_TRANSFER_FOG(o,o.pos);
//				return o;

				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uvShadow = mul(unity_Projector, v.vertex);

				float3 normView = normalize(float3(unity_Projector[2][0], unity_Projector[2][1], unity_Projector[2][2]));
				float nv = dot(v.normal, normView);
				o.nv = nv < 0 ? 1 : 0;
				
//				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 res = fixed4(1, 1, 1, texS.a );
				res.a *= i.nv;

//				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
				return res;
			}
			ENDCG
		}
	}
}
