using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Input Settings")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public KeyCode jumpInput = KeyCode.Space;
    

    [Header("Camera Settings")]
    public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";
    public ThirdPersonCamera targetCamera;

    public Camera Camera2D;

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        targetCamera.Init();

        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.Init();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateCameraInput();
        UpdateCameraMovement();

        if (GameManager.gameManager.cameraState == 0)
            characterMovement.yRotate = targetCamera.transform.eulerAngles.y;
        else
            characterMovement.yRotate = Camera2D.transform.eulerAngles.y;

        UpdateJumpInput();
        UpdateMovementInput();
        characterMovement.updateMontion();
    }

    void UpdateCameraInput()
    {
        targetCamera.rotateX = Input.GetAxis(rotateCameraXInput);
        targetCamera.rotateY = Input.GetAxis(rotateCameraYInput);
    }

    void UpdateCameraMovement()
    {
        targetCamera.Movement();
    }


    void UpdateMovementInput()
    {
        float tmpx = Input.GetAxis(horizontalInput);
        float tmpy = Input.GetAxis(verticallInput);

        characterMovement.motion.x = tmpx;
        characterMovement.motion.y = tmpy;
    }

    void UpdateJumpInput()
    {
        if (Input.GetKeyDown(jumpInput))
            characterMovement.Jump();

    }
    
}
