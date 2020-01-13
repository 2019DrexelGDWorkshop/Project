using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Input Settings")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public string jumpInput = "Jump";
    public string cameraChangeInput = "CameraChange";
    

    [Header("Camera Settings")]

    public GameObject targetCamera;
    public GameObject Camera2D;

    public GameObject cameraBrain;

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {

        characterMovement = GetComponent<CharacterMovement>();
        //characterMovement.Init();
    }

    // Update is called once per frame
    void Update()
    {
    //    UpdateJumpInput();
     //   UpdateMovementInput();

    //    characterMovement.updateTargetDirection(cameraBrain.transform);

        /*if (GameManager.Instance.cameraState == 0)
            characterMovement.updateTargetDirection(targetCamera.transform);
        else
            characterMovement.updateTargetDirection(Camera2D.transform);*/

    //    characterMovement.updateMontion();

    //    UpdateCameraStateInput();
    }

    void UpdateMovementInput()
    {
        float tmpx = Input.GetAxis(horizontalInput);
        float tmpy = Input.GetAxis(verticallInput);

        if (GameManager.Instance.cameraState == GameManager.cameraState2D)
            tmpy = 0;

        characterMovement.motion.x = tmpx;
        characterMovement.motion.y = tmpy;
    }

    private int jumpCount = -1;
    void UpdateJumpInput()
    {
        if (Input.GetButton(jumpInput))
        {
            //characterMovement.Jump();
            jumpCount = 15;
        }/*
        if (jumpCount > 0)
        {
            characterMovement.Jump();
        }
        jumpCount -= 1;*/
    }
    
    void UpdateCameraStateInput()
    {
        if (Input.GetButtonDown(cameraChangeInput))
        {
            
            GameManager.Instance.ChangeCameraState();
        }
    }

}
