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
        else
        {
            StartCoroutine(ExampleCoroutine());
            
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        rend.enabled = true;
    }
}