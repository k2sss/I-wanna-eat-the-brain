Shader "Custom/SpriteColorFilter"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _Threshold("Red Threshold", Range(0,1)) = 0.5
        _ReplaceColor("Replacement Color", Color) = (0,1,0,1) // 默认替换色为绿色
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off Lighting Off ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                    fixed4 color : COLOR;
                };

                struct v2f {
                    float2 texcoord : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Threshold;
                fixed4 _ReplaceColor;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.color = v.color;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.texcoord);

                    if (col.r+0.02 < _Threshold) {
                        col.rgb = _ReplaceColor.rgb;
                    }
                    else {
                        col.rgba = (0, 0, 0, 0);
                    }

                    return col * i.color;
                }
                ENDCG
            }
        }
}
