Shader "Custom/flow" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_FlowTex ("Flow Texture(A)", 2D) = "black" {}
		_uvadd ("", range(0, 1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _FlowTex;
		float _uvadd;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			float2 uv = IN.uv_MainTex;
			uv.x /= 2;
			uv.x += _uvadd;
			
			float flow = tex2D (_FlowTex, uv).a;
			
			o.Albedo = c.rgb + float3(flow, flow, flow);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
