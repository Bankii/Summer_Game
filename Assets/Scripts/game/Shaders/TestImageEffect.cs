using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestImageEffect : MonoBehaviour {

    public Material transitionMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, transitionMaterial);
    }
}
