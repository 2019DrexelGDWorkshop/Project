using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Input Settings")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public string jumpInput = "Jump";
    

    [Header("Camera Settings")]

    public Camera targetCamera;
    public Camera Camera2D;

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {

        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateJumpInput();
        UpdateMovementInput();

        if (GameManager.gameManager.cameraState == 0)
            characterMovement.updateTargetDirection(targetCamera.transform);
        else
            characterMovement.updateTargetDirection(Camera2D.transform);

        characterMovement.updateMontion();

    }

    void UpdateMovementInput()
    {
        float tmpx = Input.GetAxis(horizontalInput);
        float tmpy = Input.GetAxis(verticallInput);

        if (GameManager.gameManager.cameraState == GameManager.cameraState2D)
            tmpy = 0;

        characterMovement.motion.x = tmpx;
        characterMovement.motion.y = tmpy;
    }

    private int jumpCount = -1;
    void UpdateJumpInput()
    {
        if (Input.GetButton(jumpInput))
        {
            characterMovement.Jump();
            jumpCount = 15;
        }/*
        if (jumpCount > 0)
        {
            characterMovement.Jump();
        }
        jumpCount -= 1;*/
    }
    
}
