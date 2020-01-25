using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MoveAndAnchorSystem
{

    public float rateDecay = 0.2f;

    protected override void Update()
    {
        CalcDistAndDir();

        Rigidbody rb_mPlat = movingObj.GetComponent<Rigidbody>();
        if (rb_mPlat.velocity.magnitude > speed)
        {
            rb_mPlat.velocity = rb_mPlat.velocity.normalized * speed;
        }
        else if (reachedTarget)
        {
            TargetReached();
        }
        else
        {
            rb_mPlat.AddForce(speed * direction);
        }
    }

    public override void TargetReached()
    {
        movingObj.GetComponent<Rigidbody>().velocity = movingObj.GetComponent<Rigidbody>().velocity * Mathf.Pow((1 - rateDecay), Time.time - timeReached);
        print(movingObj.GetComponent<Rigidbody>().velocity);
        if (movingObj.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
        {
            reachedTarget = false;
        }
    }
}
