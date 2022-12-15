Shader "Custom/Waves"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        // Wave type beat
        _WaveA ("Wave A (direction, steepness, wavelength)", Vector) = (1, 0.2, 0.1, 10)
        _WaveB ("Wave B", Vector) = (0.3, 0, 0.1, 10)
        _WaveC ("Wave C", Vector) = (1, 0.1, 0.1, 15)

        // Foam
        _FoamColor ("Foam Color", Color) = (1,1,1,1)
        _FoamWidth ("Foam Width", Range(0,1)) = 0.7
    }
    SubShader
    {
        Tags { "RenderType"="Opaqeue" }
        LOD 200

        GrabPass { "_WaterBackground" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha vertex:vert addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "WaterDepth.cginc"

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        fixed4 _Color, _FoamColor;
        float4 _WaveA, _WaveB, _WaveC;
            
        float3 GerstnerWave (
            float4 wave, float3 p, inout float3 tangent, inout float3 binormal
        ) {
            float  steepness        = wave.z;
            float  wavelength       = wave.w;
            float  k                = 2 * UNITY_PI / wavelength;
            float  c                = sqrt(9.8 / k);
            float2 d                = normalize(wave.xy);
            float  f                = k * (dot(d, p.xz) - c * _Time.y);
            float  a                = steepness / k;
            
            tangent += float3(
                -d.x * d.x * (steepness * sin(f)),
                d.x * (steepness * cos(f)),
                -d.x * d.y * (steepness * sin(f))
            );

            binormal += float3(
                -d.x * d.y * (steepness * sin(f)),
                d.y * (steepness * cos(f)),
                -d.y * d.y * (steepness * sin(f))
            );
            
            return float3(
                d.x * (a * sin(f)),
                a * sin(f),
                d.y * (a * sin(f))
            );
        }

        void vert(inout appdata_full vertexData) {
            float3 gridPoint        = vertexData.vertex.xyz;
            float3 tangent          = float3(1, 0, 0);
            float3 binormal         = float3(0, 0, 1);
            float3 p                = gridPoint;
            p                       += GerstnerWave(_WaveA, gridPoint, tangent, binormal);
            p                       += GerstnerWave(_WaveB, gridPoint, tangent, binormal);
            p                       += GerstnerWave(_WaveC, gridPoint, tangent, binormal);
            float3 normal           = normalize(cross(binormal, tangent));
            vertexData.vertex.xyz   = p;
            vertexData.normal       = normal;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * CalcFoam(IN.screenPos) ? _FoamColor : _Color;
            o.Albedo = c.rgb;
            o.Alpha = 1; 
        }
        ENDCG
    }
}
