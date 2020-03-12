using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLockers : MonoBehaviour
{
    public GameObject RightLockers1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
        {
            RightLockers1.SetActive(false);
        }
        else
        {
            RightLockers1.SetActive(true);
        }
    }
}
