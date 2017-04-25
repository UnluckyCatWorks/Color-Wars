using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class AlphaColorSwitch : ImageEffectBase
{
	void OnRenderImage ( RenderTexture source, RenderTexture destination )
	{
		Graphics.Blit ( source, destination, material );
	}
}
