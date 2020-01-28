using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MoveAndAnchorSystem
{

    public float rateDecay = 0.2f;
    private Rigidbody rb_mPlat;

    protected override void Start()
    {
        base.Start();
        rb_mPlat = movingObj.GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        if(CameraManager.Instance.isTransitioning)
        {
            rb_mPlat.velocity = Vector3.zero;
            return;
        }

        CalcDistAndDir();

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
            rb_mPlat.AddForce(speed * direction );
        }
    }

    public override void TargetReached()
    {
        rb_mPlat.velocity = movingObj.GetComponent<Rigidbody>().velocity * Mathf.Pow((1 - rateDecay), Time.time - timeReached);
        if (rb_mPlat.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
        {
            reachedTarget = false;
        }
    }
}
