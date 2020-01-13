using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour
{
    #region Attributes


    public float moveSpeed = 6.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpUpTime = 0.5f;
    public float gravity = 10.0f;

    private bool isGrounded = true;
    public float groundMargin = 1.2f;
    public float groundDetectRadius = 1f;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float playerBottomPoint = 1f;

    [SerializeField] private LayerMask groundLayers;

    public GameObject cameraBrain;

    [HideInInspector]
    public Vector2 motion;
    public bool isjumping = false;

    CharacterController cc;

    Vector3 targetDirection = new Vector3(0, 0, 0);

    private float jumpCountDown = 0.0f;

    public Vector3 moveVec = new Vector3(0.0f, 0.0f, 0.0f);

    #endregion

    private void Start()
    {
        IsGrounded();
        cc = GetComponent<CharacterController>();
        GameManager.Instance.rewiredPlayer.AddInputEventDelegate(Jump,  UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Jump);
        GameManager.Instance.rewiredPlayer.AddInputEventDelegate(updateMotion,  UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveForward3D);
        GameManager.Instance.rewiredPlayer.AddInputEventDelegate(updateMotion,  UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveRight3D);
    }

    private void OnDestroy()
    {
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(Jump, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Jump);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveForward3D);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveRight3D);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveForward2D);
    }

    private void OnDisable()
    {
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(Jump, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Jump);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveForward3D);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveRight3D);
        GameManager.Instance.rewiredPlayer.RemoveInputEventDelegate(updateMotion, UpdateLoopType.Update, InputActionEventType.Update, RewiredConsts.Action.MoveForward2D);
    }

    private void Update()
    {
        updateTargetDirection(cameraBrain.transform);
        IsGrounded();
    }

    private void IsGrounded()
    {
        Vector3 pointBottom = transform.position;//transform.up * groundMargin + transform.up * groundDetectRadius;
       // Vector3 pointTop = transform.position + transform.up * groundDetectRadius;
        RaycastHit hit;

        Debug.DrawRay(pointBottom, Vector3.down * groundCheckDistance, Color.red, 1);

        if(Physics.SphereCast(pointBottom, groundDetectRadius, Vector3.down, out hit, groundCheckDistance, groundLayers))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Debug.Log(isGrounded);
    }

    private void updateMotion(InputActionEventData _eventData)
    {

        motion.x = GameManager.Instance.rewiredPlayer.GetAxis(RewiredConsts.Action.MoveRight3D);
        motion.y = GameManager.Instance.rewiredPlayer.GetAxis(RewiredConsts.Action.MoveForward3D);

        //Prevent running many times per frame.
        if (_eventData.actionId == RewiredConsts.Action.MoveForward3D)
        {
            return;
        }
            /*if(Mathf.Abs(motion.y) <= Mathf.Abs(motion.x))
            {
                return;
            }
        }
        else if (_eventData.actionId == RewiredConsts.Action.MoveRight3D)
        {
            if(Mathf.Abs(motion.x) <= Mathf.Abs(motion.y))
            {
                return;
            }
        }*/

        updateRotation();

        float tmpSpeed = Mathf.Abs(motion.x) + Mathf.Abs(motion.y);
        tmpSpeed = Mathf.Clamp(tmpSpeed, 0, 1f);
        motion.Normalize();

        float tmpy = moveVec.y;

        if (GameManager.Instance.cameraState == GameManager.cameraState2D)
        {
            moveVec = targetDirection * tmpSpeed * moveSpeed;
        }
        else
        { 
            moveVec = transform.forward * tmpSpeed * moveSpeed;
        }

        /*if (isjumping)
        {
            jumpCountDown -= Time.deltaTime;
            if (jumpCountDown <= 0.0f)
            {
                isjumping = false;
                moveVec.y = 0;
            }
            moveVec.y = Mathf.Lerp(0, jumpSpeed, jumpCountDown / jumpUpTime);

            //moveVec.y = tmpy - gravity * Time.deltaTime;
        }*/
        //else if (!isjumping && !isGrounded)
        //  moveVec.y = tmpy - gravity * Time.deltaTime;
        //else
        //    moveVec.y = 0;

        if(isjumping)
        {
            Debug.Log("Jumping");
            moveVec.y = jumpSpeed;
            isjumping = false;
        }
        else if(!isGrounded)
        {
            Debug.Log("Not Grounded");
            moveVec.y = moveVec.y - (gravity * Time.deltaTime);
        }
        else
        {
            Debug.Log("Nothing");
            moveVec.y = 0;
        }

        cc.Move(moveVec * Time.deltaTime);
        
    }

    private void updateRotation()
    {
        //Debug.Log(targetDirection);
        if (targetDirection.magnitude <= 0)
            return;
        Vector3 lookDirection = targetDirection.normalized;
        Quaternion rotationAngle = Quaternion.LookRotation(lookDirection, transform.up);

        float diffRotation = rotationAngle.eulerAngles.y - transform.eulerAngles.y;
        float eulerY = Mathf.Abs(diffRotation) > 0 ? rotationAngle.eulerAngles.y : transform.eulerAngles.y;
        Vector3 euler = new Vector3(transform.eulerAngles.x, eulerY, transform.eulerAngles.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotationSpeed * Time.deltaTime);
    }

    private void updateAnimation()
    {

    }

    private void Jump(InputActionEventData _eventData)
    {
        if (!isGrounded)
        {
            return;
        }
        //jumpCountDown
        isjumping = true;
    }

    private void updateTargetDirection(Transform referenceTrans)
    {
        Vector3 forward = referenceTrans.TransformDirection(Vector3.forward);
        forward.y = 0;

        Vector3 right = referenceTrans.TransformDirection(Vector3.right);

        targetDirection = motion.x * right + motion.y * forward;
    }

}
