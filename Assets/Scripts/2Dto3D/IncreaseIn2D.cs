using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseIn2D : MonoBehaviour
{
    [SerializeField]
    GameObject ColliderIn2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            ColliderIn2D.SetActive(true);
        }
        else
        {
            ColliderIn2D.SetActive(false);
        }
    }

    public void setFalse()
    {
        ColliderIn2D.SetActive(false);
    }
}
