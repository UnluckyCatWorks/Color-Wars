Shader "Projector/ProjectAlpha"
{
	Properties
	{
		_ShadowTex ("Cookie", 2D) = "gray" {}
	}
	Subshader
	{
		Tags { "Queue"="Transparent"}
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
		   };

			struct v2f
			{
				float4 uvShadow : TEXCOORD0;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
				fixed nv : COLOR0;
			};
			
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			
			v2f vert (Input v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uvShadow = mul(unity_Projector, v.vertex);
				UNITY_TRANSFER_FOG(o,o.pos);

				// For me, splatters were being projected on both sides of the
				// object, so I used the view direction and the surface normal
				// to check if it was facing the camera.
				float3 normView = normalize(float3(unity_Projector[2][0], unity_Projector[2][1], unity_Projector[2][2]));
				float nv = dot(v.normal, normView);
				// negative values means surface isn't facing the camera
				o.nv = nv < 0 ? 1 : 0;
				
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 res = fixed4(1, 1, 1, texS.a );
				// Multiply by alpha channel to
				// remove back-side projection.
				res.a *= i.nv;

				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
				return res;
			}
			ENDCG
		}
	}
}
