using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialQueue : MonoBehaviour
{
	private static int queue = 2000 + 1;
	private Material mat;

	void Awake ()
	{
		mat = GetComponent<Renderer> ().sharedMaterial;
		mat.renderQueue = ++queue;
	}
}
