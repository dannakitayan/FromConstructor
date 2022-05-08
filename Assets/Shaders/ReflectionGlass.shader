Shader "DonakiShaders/ReflectionGlass"
{
    Properties{
    _Color("Main Color", Color) = (1,1,1,1)
    _MainTex("Base (RGB)", 2D) = "white" {}
    _CubeTex("Cubemap", cube) = "" {}
    _Illum("Illumin (A)", 2D) = "white" {}
    _Emission("Emission (Lightmapper)", Float) = 1.0
    _ReflectColor("Reflection Color", Color) = (1,1,1,0.5)

    }
        SubShader{
            Tags {"RenderType" = "TransparentCutout"}
            LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _Illum;
        samplerCUBE _CubeTex;
        fixed4 _Color;
        fixed4 _ReflectColor;
        fixed _Emission;

        struct Input {
            float2 uv_MainTex;
            float2 uv_Illum;
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 c = tex * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            fixed4 reflcol = texCUBE(_CubeTex, IN.worldRefl);
            reflcol *= tex.a;
            o.Emission = reflcol.rgb * tex2D(_Illum, IN.uv_Illum).a * _Emission;
            o.Alpha = reflcol.a * _ReflectColor.a * tex.a;
        }
        ENDCG
    }
}
