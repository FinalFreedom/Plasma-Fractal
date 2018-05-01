Shader "Custom/Map" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_SnowC("Snow", Color) = (0.9,0.9,0.9,1)
		_SnowL("SnowLv", Float) = 50
		_RockC("Rock", Color) = (0.1,0.3,0.4,1)
		_RockL("RockLv", Float) = 45
		_GrassC("Grass", Color) = (0.9,0.3,0.6,1)
		_GrassL("GrassLv", Float) = 42
		_SandC("Sand", Color) = (0.4,0.9,0.5,1)
		_SandL("SandLv", Float) = 35.5
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Lambert

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float3 worldPos;
			float2 uv_MainTex;
		};

		float4 _SnowC;
		float _SnowL;
		float4 _RockC;
		float _RockL;
		float4 _GrassC;
		float _GrassL;
		float4 _SandC;
		float _SandL;
		void surf(Input IN, inout SurfaceOutput o)
		{

			if(IN.worldPos.y >= _RockL)
				o.Albedo = lerp(_SnowC, _RockC, -(IN.worldPos.y - _SnowL) / (_SnowL - _RockL));
				o.Albedo *= tex2D(_MainTex, IN.uv_MainTex);
			if (IN.worldPos.y <= _RockL)
				o.Albedo = lerp(_RockC,_GrassC,-(IN.worldPos.y-_RockL)/(_RockL - _GrassL));
				o.Albedo *= tex2D(_MainTex, IN.uv_MainTex);
			if (IN.worldPos.y <= _GrassL && IN.worldPos.y > _SandL)
				o.Albedo = _GrassC;
				o.Albedo *= tex2D(_MainTex, IN.uv_MainTex);
			if (IN.worldPos.y <= _SandL)
				o.Albedo = _SandC;
		}	
		ENDCG
	}
	FallBack "Diffuse"
}
