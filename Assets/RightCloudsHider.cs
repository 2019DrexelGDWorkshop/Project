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
        if (GameManager.gameManager.cameraState == 1)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
    }
}
