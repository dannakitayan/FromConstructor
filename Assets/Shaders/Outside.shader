Shader "Unlit/Outside"
{
    Properties {
        _MainTex ("Cubemap", Cube) = "white" {}
        _Frame ("Frame texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 100
 
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uvFrame : TEXCOORD1;
            };
 
            struct v2f {
                float3 worldPos : TEXCOORD0;
                float2 uvFrame : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
 
            samplerCUBE _MainTex;
            sampler2D _Frame;
            float4 _Frame_ST;

 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uvFrame = TRANSFORM_TEX(v.uvFrame, _Frame);
                return o;
            }
 
            float4 frag (v2f i) : SV_Target {
                float3 worldNormal = normalize(i.worldPos - _WorldSpaceCameraPos.xyz);
                float4 col = texCUBElod(_MainTex, float4(worldNormal, 0));
                fixed4 frameColor = tex2D(_Frame, i.uvFrame);
                col = lerp(col, frameColor, frameColor.a);
                return col;
            }
            ENDCG
        }
    }
}
