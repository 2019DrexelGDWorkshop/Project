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

        if (GameManager.gameManager.cameraState == 0)
            characterMovement.yRotate = targetCamera.transform.eulerAngles.y;
        else
            characterMovement.yRotate = Camera2D.transform.eulerAngles.y;

        UpdateJumpInput();
        UpdateMovementInput();

    }

    private void FixedUpdate()
    {
        characterMovement.updateMontion();
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
        if (Input.GetButtonDown(jumpInput))
            characterMovement.Jump();

    }
    
}
