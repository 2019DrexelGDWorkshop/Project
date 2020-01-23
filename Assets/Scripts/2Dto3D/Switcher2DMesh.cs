using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher2DMesh : MonoBehaviour
{
    public GameObject MeshColliderIn2D;
    //public GameObject MeshColliderIn3D;

    void Update()
    {
        if (GameManager.Instance.cameraState == 1)
        {
            MeshColliderIn2D.SetActive(true);
            //MeshColliderIn2D.SetActive(false);
        }
        else
        {
            MeshColliderIn2D.SetActive(false);
            //MeshColliderIn2D.SetActive(true);
        }
    }

    public void setFalse()
    {
        MeshColliderIn2D.SetActive(false);
    }
}
