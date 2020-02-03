using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesOpenScript : MonoBehaviour
{
    Image eyeImage;
    public Sprite eye1;
    public Sprite eye2;


    // Start is called before the first frame update
    void Start()
    {
        eyeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.Instance.cameraState == CameraState.THIRD_PERSON)
        {
            eyeImage.sprite = eye2;
        }
        else
        {
            eyeImage.sprite = eye1;
        }
    }
}
