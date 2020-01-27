using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{

    [Header("Rewired")]
    [SerializeField] private int rewiredPlayerId = 0;
    private Player rewiredPlayer;
    

    [Header("Camera Settings")]

    private CharacterMovement characterMovement;


    // Start is called before the first frame update

    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
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

        try
        {
            characterMovement.updateTargetDirection(CameraManager.Instance.cmBrain.transform);

            characterMovement.updateMontion();

            UpdateCameraStateInput();
        }catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    void UpdateMovementInput()
    {
        float tmpx = rewiredPlayer.GetAxis(RewiredConsts.Action.MoveRight);
        float tmpy = rewiredPlayer.GetAxis(RewiredConsts.Action.MoveForward);

        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
            tmpy = 0;

        characterMovement.motion.x = tmpx;
        characterMovement.motion.y = tmpy;
    }

    private int jumpCount = -1;
    void UpdateJumpInput()
    {
        if(rewiredPlayer.GetButtonDown(RewiredConsts.Action.Jump))
        {
            characterMovement.Jump();
            jumpCount = 15;
        }
    }
    
    void UpdateCameraStateInput()
    {
        if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.PerspectiveSwitch) && !CameraManager.Instance.isTransitioning)
        {
            CameraManager.Instance.Toggle3D2D();
        }
    }

}
