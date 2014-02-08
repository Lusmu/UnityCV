Shader "Lusmu/Transparent Unlit"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1) 
		_MainTex ("Texture", 2D) = "white" {}   		
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
			half _YMin;
			half _YMax;
			sampler2D _MainTex;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float height;
			};

			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;

				output.tex = input.texcoord;
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				output.height = mul (_Object2World, input.vertex).y;	
				
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				fixed heightFog = 1 - (input.height - _YMin) / (_YMax - _YMin);
				heightFog = clamp(heightFog, 0, 1);

				float4 textureColor = tex2D(_MainTex, float2(input.tex));
				textureColor.a -= heightFog;
				return textureColor * _Color;
			}
        
		ENDCG
		}
	}
	
	FallBack "Unlit/Transparent"
}