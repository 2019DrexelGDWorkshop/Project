using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingShooter : Shooter_base
{
    [SerializeField, Tooltip("The minimum horizontal angle between the player and the forward of the shooter.")]
    private float minHorizontalAngle = 10f;

    private Quaternion oldBodyRotation = Quaternion.identity;


    private float oldAngle = 0;
    private float smoothTime = 2f;
    private float percentage = 0;
        
    protected override void Aim(Vector3 _pos)
    {
        Vector3 bodyForward = shooterBody.transform.forward;
        Vector3 bodyToPos = _pos - shooterBody.transform.position;

        
        Debug.DrawRay(shooterBody.transform.position, bodyToPos, Color.red);

        float angle = Vector3.Angle(bodyForward, bodyToPos);

        if (oldBodyRotation == Quaternion.identity)
        {
            oldBodyRotation = shooterBody.transform.rotation;
            oldAngle = angle;
        }

        if(angle < minHorizontalAngle)
        {
            Shoot();
            oldBodyRotation = Quaternion.identity;
            oldAngle = 0;
            percentage = 0;
            return;
        }

        Vector3 currRotation = shooterBody.transform.rotation.eulerAngles;
        if (angle > 0)
        {
            angle *= -1;
        }
        Vector3 targetRotation = new Vector3(0, currRotation.y + angle, 0);

        shooterBody.transform.rotation = Quaternion.Slerp(shooterBody.transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * smoothTime);


    }
}
