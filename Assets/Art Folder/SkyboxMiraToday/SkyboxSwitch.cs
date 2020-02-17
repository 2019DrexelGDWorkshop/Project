using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSwitch : MonoBehaviour
{
    public Material skybox;
    Renderer s_renderer;
    public bool isWaiting = false;
    public Texture[] frames;
    public int frameCounter = 0;

    void Start()
    {
        RenderSettings.skybox = skybox;
        s_renderer = GetComponent<Renderer> ();
    }

    void LateUpdate()
    {
        if (!isWaiting)
        {
            StartCoroutine(ChangeFrame());
        }   
    }

    IEnumerator ChangeFrame()
    {
        isWaiting = true;
        
        if (frameCounter == frames.Length)
        {
            frameCounter = 0;
        }
        skybox.SetTexture("_FrontTex", frames[frameCounter]);
        skybox.SetTexture("_BackTex", frames[frameCounter]);
        skybox.SetTexture("_LeftTex", frames[frameCounter]);
        skybox.SetTexture("_RightTex", frames[frameCounter]);
        //skybox.SetTexture("_UpTex", frames[frameCounter]);
        //skybox.SetTexture("_DownTex", frames[frameCounter]);
        yield return new WaitForSeconds(.33f);
        frameCounter++;
        isWaiting = false;

    }

}
