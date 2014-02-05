Shader "Lusmu/Cube" {
	Properties {
		_Color ("Color tint", Color) = (1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		fixed3 _Color;
		half _YMin;
		half _YMax;

		struct Input {
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {				
			fixed heightFog = 1 - (IN.worldPos.y - _YMin) / (_YMax - _YMin);
			heightFog = clamp(heightFog, 0, 10);
			
			o.Albedo = _Color;
			o.Emission = half3(heightFog);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
