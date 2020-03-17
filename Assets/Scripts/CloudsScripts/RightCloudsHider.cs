using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCloudsHider : MonoBehaviour
{
    public Renderer rend;


    void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            rend.enabled = false;
        }
        else if(CameraManager.Instance.cameraState == CameraState.TRANSITION)
        {
            StartCoroutine(WaitThenOff());
        }
        else
        {
            StartCoroutine(ExampleCoroutine());
            
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        rend.enabled = true;
    }

    IEnumerator WaitThenOff()
    {
        yield return new WaitForSeconds(.2f);
        rend.enabled = false;
    }
}