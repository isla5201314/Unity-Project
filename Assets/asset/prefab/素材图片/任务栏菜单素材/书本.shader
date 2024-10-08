Shader "Unlit/书本双面材质修正"
{
    Properties
    {
        _MainTex("主纹理(正面)", 2D) = "white" {}
        _BackMainTex("背面主纹理", 2D) = "white" {}
        _NextTex("下一页(正面)", 2D) = "white" {}
        _BackNextTex("背面下一页", 2D) = "white" {}
        _Angle("旋转角度", Range(0,180)) = 0
    }

        SubShader
        {
            Tags {"RenderType" = "Opaque" "Queue" = "Geometry"}

            Pass // 正面和背面Pass合并
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : COLOR; // 添加颜色属性用于标记正面和背面
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    fixed4 color : COLOR;
                };

                sampler2D _MainTex;
                sampler2D _NextTex;
                sampler2D _BackMainTex;
                sampler2D _BackNextTex;
                float _Angle;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.color = v.color; // 传递颜色到片段着色器
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 tex;
                // 假设正面颜色为(1,0,0,0)，背面颜色为(0,0,1,0)
                bool isFrontFace = i.color.r > 0 && i.color.g == 0;

                if (isFrontFace) {
                    tex = lerp(tex2D(_MainTex, i.uv), tex2D(_NextTex, i.uv), _Angle / 180);
                }
 else {
  tex = lerp(tex2D(_BackMainTex, i.uv), tex2D(_BackNextTex, i.uv), _Angle / 180);
}
return tex;
}
ENDCG
}
        }
}