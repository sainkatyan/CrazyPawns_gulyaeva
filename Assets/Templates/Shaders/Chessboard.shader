Shader "Custom/Chessboard"
{
    Properties
    {
        _Scale ("Scale", Float) = 1.5
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 worldPos : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Scale;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                o.worldPos = worldPos.xz;
                
                return o;
            }

            float Chessboard(float2 pos)
            {
                float2 gridPos = pos / _Scale; 
                float xOffset = step(frac(gridPos.y), 0.5) * 0.5;
                return step(frac(gridPos.x + xOffset), 0.5);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float value = Chessboard(i.worldPos);
                return lerp(_Color1, _Color2, value);
            }

            ENDCG
        }
    }
}
