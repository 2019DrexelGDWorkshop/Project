using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float jumpUpTime = 0.5f;
    public float gravity = 10.0f;

    [HideInInspector]
    public Vector2 motion;
    public bool isjumping = false;

    CharacterController cc;

    public void Init()
    {
        cc = GetComponent<CharacterController>();
    }

    public float yRotate;

    public void updateMontion()
    {
        transform.eulerAngles = new Vector3(0, yRotate, 0);    // TTTTTTTTTTTT

        Vector3 moveVec = (motion.y * transform.forward + motion.x * transform.right) * moveSpeed;
        //Vector3 moveVec = new Vector3(motion.x * transform.forward.x, 0.0f, motion.y * transform.forward.z) * moveSpeed; // TTTTTTTTTTTTTTTT

        if (isjumping)
        {
            jumpCountDown -= Time.deltaTime;
            if (jumpCountDown <= 0.0f)
                isjumping = false;
            moveVec.y = jumpSpeed;
        }
        
        moveVec.y -= gravity * Time.deltaTime;

        cc.Move(moveVec);
        
    }

    public void updateAnimation()
    {

    }

    private float jumpCountDown = 0.0f;
    public void Jump()
    {
        if (!cc.isGrounded)
            return;
        isjumping = true;
        jumpCountDown = jumpUpTime;
    }

}
