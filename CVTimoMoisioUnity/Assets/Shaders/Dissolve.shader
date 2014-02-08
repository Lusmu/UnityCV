Shader "Lusmu/Dissolve" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {} 
		_AlphaControl ("Alpha control", 2D) = "white" {} 
   		_Color ("Color", Color) = (1,1,1,1) 
   		_Threshold ("Threshold", Range(0,1)) = 1
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" } 
		Lighting Off
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _AlphaControl;
		fixed _Threshold;
		
		struct Input {
			float3 worldPos;
			fixed2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {			
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed dissolve = tex2D(_AlphaControl, IN.uv_MainTex).a;
			
			if (_Threshold < dissolve) dissolve = 0;
			else dissolve = clamp(dissolve + _Threshold, 0, 1);
			
			o.Albedo = _Color.rgb * tex.rgb;
			o.Alpha = dissolve * _Color.a * tex.a;
			o.Normal = 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
