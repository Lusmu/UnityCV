Shader "Lusmu/Dissolve"
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {} 
		_AlphaControl ("Alpha control", 2D) = "white" {} 
   		_Color ("Color", Color) = (1,1,1,1) 
   		_Threshold ("Threshold", Range(0,1)) = 1
	}
	SubShader 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" } 
		Lighting Off
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 
	
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			fixed _Threshold;
			sampler2D _MainTex;
			sampler2D _AlphaControl;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			};

			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;

				output.tex = input.texcoord;
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				fixed dissolve = tex2D(_AlphaControl, float2(input.tex)).a;
				if (_Threshold < dissolve) dissolve = 0;
				else dissolve = clamp(dissolve + _Threshold, 0, 1);
				
				float4 textureColor = tex2D(_MainTex, float2(input.tex));
				return textureColor * _Color * dissolve;
			}
        
			ENDCG
		}
	}
	
	FallBack "Unlit/Transparent"
}