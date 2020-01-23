using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesOpenScript : MonoBehaviour
{
    Image eyeImage;
    public Camera cam3D;
    public GameObject spriteToHide;
    public Sprite eye1;
    public Sprite eye2;
    public bool threedyORtwody;


    // Start is called before the first frame update
    void Start()
    {
        eyeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.cameraState == 0)
        {
            eyeImage.sprite = eye2;
        }
        else
        {
            eyeImage.sprite = eye1;
        }
    }
}
