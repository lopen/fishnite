#if !defined(LOOKING_THROUGH_WATER_INCLUDED)
#define LOOKING_THROUGH_WATER_INCLUDED

sampler2D _CameraDepthTexture;
float4 _CameraDepthTexture_TexelSize;
float _FoamWidth;

bool CalcFoam (float4 screenPos) {
	float2 uv = screenPos.xy / screenPos.w;

	float backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
	float surfaceDepth = UNITY_Z_0_FAR_FROM_CLIPSPACE(screenPos.z);
	float depthDifference = backgroundDepth - surfaceDepth;


	// if its closest to the object
	if ((depthDifference < _FoamWidth) && backgroundDepth < 50) {
		//if (depthDifference > _FoamWidth - 0.3f && depthDifference < _FoamWidth - 0.1f) {
			return true;
		//}
	}
	return false; 
}

#endif