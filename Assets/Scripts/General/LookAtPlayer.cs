using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    [SerializeField] private float minHorizontalAngle = 0;
    [SerializeField] private float smoothTime = 2;
    private Vector3 playerPos;
    private float oldAngle = 0;
    private Quaternion oldBodyRotation = Quaternion.identity;
    private float percentage = 0;

    private void Start()
    {
        
    }


    void FixedUpdate()
    {
        playerPos = LevelManager.Instance.player.transform.position;
        Vector3 bodyForward = transform.forward;
        Vector3 bodyToPos = playerPos - transform.position;
        bodyToPos.y = 0;


        Debug.DrawRay(transform.position, bodyToPos, Color.red);

        float angle = Vector3.Angle(bodyForward, bodyToPos);

        if (oldBodyRotation == Quaternion.identity)
        {
            oldBodyRotation = transform.rotation;
            oldAngle = angle;
        }

        Debug.Log(angle);
        if (angle < minHorizontalAngle)
        {
            oldBodyRotation = Quaternion.identity;
            oldAngle = 0;
            percentage = 0;
            return;
        }

        Vector3 currRotation = transform.rotation.eulerAngles;
        if (angle > 0)
        {
            angle *= -1;
        }
        Vector3 targetRotation = new Vector3(0, currRotation.y + angle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * smoothTime);
    }
}
