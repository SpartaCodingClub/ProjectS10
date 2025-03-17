Shader "Custom/DissolveShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve Texture", 2D) = "gray" {} 
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _EdgeColor ("Edge Color", Color) = (1, 0, 0, 1) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _DissolveTex;
        float _DissolveAmount;
        fixed4 _EdgeColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float dissolveValue = tex2D(_DissolveTex, IN.uv_MainTex).r; 
            float dissolveThreshold = _DissolveAmount;

            if (dissolveValue < dissolveThreshold + 0.05 && dissolveValue > dissolveThreshold)
            {
                o.Emission = _EdgeColor.rgb; 
            }

            clip(dissolveValue - dissolveThreshold); 
            o.Albedo = c.rgb;
        }
        ENDCG
    }
}
