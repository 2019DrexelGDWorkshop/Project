using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher2DMesh : MonoBehaviour
{
    public GameObject MeshColliderIn2D;

    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            MeshColliderIn2D.SetActive(true);
        }
        else
        {
            MeshColliderIn2D.SetActive(false);
        }
    }

    public void setFalse()
    {
        MeshColliderIn2D.SetActive(false);
    }
}
