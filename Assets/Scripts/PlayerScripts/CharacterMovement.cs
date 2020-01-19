using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpUpTime = 0.5f;
    public float gravity = 10.0f;
    public float jetpackForce = 1.0f;

    //public bool isGrounded = false;
    public float groundMargin = 1.2f;
    public float groundDetectRadius = 0.5f;

    [HideInInspector]
    public Vector2 motion;
    public bool isjumping = false;
    private bool isFlying = false;
    private float delayJetpack = 0.0f;
    private float timer = 0.0f;

    CharacterController cc;

    Vector3 targetDirection = new Vector3(0, 0, 0);
    private float currentJetpackForce = 0.0f;

    public void Init()
    {
        delayJetpack = jetpackForce;
        cc = GetComponent<CharacterController>();
    }

    /*public void changeGroundState(bool newState)
    {
        isGrounded = newState;
    }*/

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            timer += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            timer = 0;
        }
        if (isjumping)
        {
            if (timer > 0.3f)
            {
                ActivateFlying();
            }
        }
        JetPack();
    }

    private void ActivateFlying()
    {
        isFlying = true;
    }

    bool IsGrounded()
    {
        Vector3 pointBottom = transform.position - transform.up * groundMargin + transform.up * groundDetectRadius;
        Vector3 pointTop = transform.position + transform.up * groundDetectRadius;
        LayerMask ignoreMask = ~(1 << 9);

        //Debug.DrawLine(pointBottom, pointTop, Color.green);
        //Debug.Log(Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore)[0]);
        if (Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore).Length != 0)
        {
            //Debug.Log(Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore)[0]);
            return true;
        }
        else
            return false;

        //return isGrounded;
        //isGrounded |= Physics.Raycast(transform.position, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);
        //isGrounded |= Physics.Raycast(transform.position + transform.forward, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);
        //return Physics.Raycast(transform.position, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);   // Watch out the layer!!!
    }

    bool IsGroundedJump()
    {
        Vector3 pointBottom = transform.position - transform.up * groundMargin + transform.up * groundDetectRadius;
        Vector3 pointTop = transform.position + transform.up * groundDetectRadius;
        LayerMask ignoreMask = ~(1 << 9);

        if (Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius + 0.1f, ignoreMask, QueryTriggerInteraction.Ignore).Length != 0)
        {
            return true;
        }
        else
            return false;
    }

    public Vector3 moveVec = new Vector3(0.0f, 0.0f, 0.0f);
    public void updateMontion()
    {
        //Debug.Log(IsGrounded());

        updateRotation();

        float tmpSpeed = Mathf.Abs(motion.x) + Mathf.Abs(motion.y);
        tmpSpeed = Mathf.Clamp(tmpSpeed, 0, 1f);

        float tmpy = moveVec.y;

        if (GameManager.Instance.cameraState == GameManager.cameraState2D)
            moveVec = targetDirection * tmpSpeed * moveSpeed;
        else
            moveVec = transform.forward * tmpSpeed * moveSpeed;

        if (isjumping)
        {
            jumpCountDown -= Time.deltaTime;
            if (jumpCountDown <= 0.0f)
            {
                isjumping = false;
                moveVec.y = 0;
            }
            moveVec.y = Mathf.Lerp(0, jumpSpeed, jumpCountDown / jumpUpTime);
        }
        else if (!isjumping && !IsGrounded())
            moveVec.y = tmpy - gravity * Time.deltaTime;
        else
            moveVec.y = 0;
        cc.Move(moveVec * Time.deltaTime);

    }

    void updateRotation()
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

    private void JetPack()
    {
        if (isFlying)
        {
            if (Input.GetButton("Jump") && jetpackForce > 0)
            {
                GetComponentInChildren<Animator>().enabled = false;
                jetpackForce -= Time.deltaTime;
                if (currentJetpackForce < 1)
                {
                    currentJetpackForce += Time.deltaTime * 10.0f;
                }
                else
                {
                    currentJetpackForce = 1.0f;
                }
            }
            if (jetpackForce < 0 && currentJetpackForce > 0)
            {
                currentJetpackForce -= Time.deltaTime;
            }
            if (!Input.GetButton("Jump"))
            {
                if (currentJetpackForce > 0)
                {
                    currentJetpackForce -= Time.deltaTime;
                }
                else
                {
                    currentJetpackForce = 0;
                }
                if (jetpackForce < delayJetpack)
                {
                    jetpackForce = delayJetpack;
                }
                else
                {
                    jetpackForce = delayJetpack;
                }
                isFlying = false;
            }
            if (currentJetpackForce > 0)
            {
                moveVec = Vector3.up;
                if (GameManager.Instance.cameraState == 1)
                {
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                        transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
                }
                if (GameManager.Instance.cameraState == 0)
                {
                    moveVec += transform.right * Input.GetAxis("Horizontal");
                    moveVec += transform.forward * Input.GetAxis("Vertical");
                }

                cc.Move((moveVec * moveSpeed * Time.deltaTime - cc.velocity * Time.deltaTime) * currentJetpackForce);
            }
        }
        else
        {
            GetComponentInChildren<Animator>().enabled = true;
        }
    }

    public void updateAnimation()
    {

    }

    private float jumpCountDown = 0.0f;
    public void Jump()
    {
        if (!IsGroundedJump())
            return;
        isjumping = true;
        jumpCountDown = jumpUpTime;
    }

    public void updateTargetDirection(Transform referenceTrans)
    {
        Vector3 forward = referenceTrans.TransformDirection(Vector3.forward);
        forward.y = 0;

        Vector3 right = referenceTrans.TransformDirection(Vector3.right);

        targetDirection = motion.x * right + motion.y * forward;
    }

}