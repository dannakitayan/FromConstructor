Shader "Custom/DetailTextureReflection"
{
    Properties
    {
        _MainTex ("Main (RGB)", 2D) = "white" {}
        _DetalTex ("Detail (RGB)", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("Normalmap", 2D) = "bump" {}
        _BumpPower ("Power", Range(0,2)) = 0.5
        [NoScaleOffset] _ParallaxMap ("Heightmap (A)", 2D) = "black" {}
        _Parallax ("Height", Range (0.005, 1)) = 0.02
        _CubeTex("Cubemap", cube) = "" {}
        _Illum("Illumin (A)", 2D) = "white" {}
        _Emission("Emission (Lightmapper)", Float) = 1.0
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
        sampler2D _Illum;
        samplerCUBE _CubeTex;
        half _BumpPower;
        float _Parallax;
        fixed4 _ReflectColor;
        fixed _Emission;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DetalTex;
            float3 viewDir;
            float2 uv_Illum;
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

            fixed4 reflcol = texCUBE(_CubeTex, -IN.viewDir);
            reflcol *= c.a;
            o.Emission = reflcol.rgb * tex2D(_Illum, IN.uv_Illum).a * _Emission;
            o.Alpha = reflcol.a * _ReflectColor.a * c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
