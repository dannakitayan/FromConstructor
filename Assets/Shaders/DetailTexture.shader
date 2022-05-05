Shader "Custom/DetailTexture"
{
    Properties
    {
        _MainTex ("Main (RGB)", 2D) = "white" {}
        _DetalTex ("Detail (RGB)", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("Normalmap", 2D) = "bump" {}
        _BumpPower ("Power", Range(0,2)) = 0.5
        [NoScaleOffset] _ParallaxMap ("Heightmap (A)", 2D) = "black" {}
        _Parallax ("Height", Range (0.005, 1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert fullforwardshadows
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        sampler2D _DetalTex;
        sampler2D _BumpMap;
        sampler2D _ParallaxMap;
        half _BumpPower;
        float _Parallax;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DetalTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            half h = tex2D (_ParallaxMap, IN.uv_DetalTex).w;
            float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
            IN.uv_MainTex += offset;
            IN.uv_DetalTex += offset;
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 g = tex2D (_DetalTex, IN.uv_DetalTex);
            o.Albedo = lerp(c.rgb, g.r, g.g);
            fixed4 b = tex2D(_BumpMap, IN.uv_DetalTex);
            o.Normal = UnpackNormalWithScale(b, _BumpPower);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
