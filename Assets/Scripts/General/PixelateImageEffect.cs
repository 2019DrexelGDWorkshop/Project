using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelateImageEffect : MonoBehaviour
{
    public Material effectMaterial;
    public int decInt = 200;
    public bool goDown;
    public GameObject GOofThis;

    void Start()
    {
        decInt = 200;
        goDown = true;
    }
    public void resetPixo()
    {
        decInt = 200;
        goDown = true;
    }
    void LateUpdate()
    {
        effectMaterial.SetFloat("_Columns", decInt);
        effectMaterial.SetFloat("_Rows", decInt);
        if (goDown)
        {
            decInt -= 30;
        }
        if (!goDown)
        {
            decInt += 3;
        }
        if (decInt <= 0)
        {
            goDown = false;
        }
        if (decInt >= 500)
        {
            decInt = 200;
            goDown = true;
            GOofThis.GetComponent<PixelateImageEffect>().enabled = false;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, effectMaterial);
    }
}
