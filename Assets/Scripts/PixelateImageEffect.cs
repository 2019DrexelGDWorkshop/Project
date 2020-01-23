using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelateImageEffect : MonoBehaviour
{
    public Material effectMaterial;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, effectMaterial);
    }
}
