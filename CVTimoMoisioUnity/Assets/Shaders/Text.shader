Shader "Lusmu/Text" {
	Properties {
		_MainTex ("Font Texture", 2D) = "white" {} 
   		_Color ("Text Color", Color) = (1,1,1,1) 
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" } 
		Lighting Off Cull Off ZWrite Off Fog { Mode Off } 
		Blend SrcAlpha OneMinusSrcAlpha 
		
		CGPROGRAM
		#pragma surface surf Lambert

		fixed3 _Color;
		half _YMin;
		half _YMax;
		sampler2D _MainTex;
		
		struct Input {
			float3 worldPos;
			fixed2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			fixed heightFog = 1 - (IN.worldPos.y - _YMin) / (_YMax - _YMin);
			heightFog = clamp(heightFog, 0, 1);
			
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = _Color + tex.rgb;
			o.Alpha = tex.a - heightFog;
			o.Normal = 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
