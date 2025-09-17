using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NoiseEffect : MonoBehaviour
{
    public Material noiseMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (noiseMaterial != null)
            Graphics.Blit(src, dest, noiseMaterial);
        else
            Graphics.Blit(src, dest);
    }
}

