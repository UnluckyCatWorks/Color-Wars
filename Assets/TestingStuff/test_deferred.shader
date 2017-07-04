Shader "TEST/test_deferred"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag_mrt
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct FragmentOutput
			{
				half4 dest0 : SV_TARGET0;
				half4 dest1 : SV_TARGET2;
			};
			
			FragmentOutput frag_mrt(v2f_img i) : SV_TARGET
			{
				FragmentOutput o;
				o.dest0 = tex2D (_MainTex, i.uv);
				o.dest1 = float4 (1, 0, 0, 1);
				return o;
			}

			ENDCG
		}
	}
}
