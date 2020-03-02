using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher2DColliderOnly : MonoBehaviour
{
    public GameObject MeshColliderIn2D;

    private void Awake()
    {
        MeshColliderIn2D.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            MeshColliderIn2D.GetComponent<BoxCollider>().enabled = true; 
        }
        else
        {
           MeshColliderIn2D.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void setFalse()
    {
        MeshColliderIn2D.GetComponent<BoxCollider>().enabled = false;
    }
}
