Shader "Lusmu/Ball" {
	Properties {
		_Color ("Color tint", Color) = (1,1,1)
		_RimColor ("Rim color", Color) = (0,0,0)
		_ViewOffset ("View offset", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		fixed3 _RimColor;
		fixed3 _Color;
		half _ViewOffset;
		half _YMin;
		half _YMax;

		struct Input {
			fixed3 viewDir;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {				
			fixed heightFog = 1 - (IN.worldPos.y - _YMin) / (_YMax - _YMin);
			heightFog = clamp(heightFog, 0, 10);
			
			half viewDot = -dot(normalize(IN.viewDir), o.Normal) * 0.5 + _ViewOffset;
			
			o.Albedo = _Color + (clamp(viewDot, 0, 1)) * _RimColor;
			o.Emission = half3(heightFog);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
