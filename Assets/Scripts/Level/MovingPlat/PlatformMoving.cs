using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MoveAndAnchorSystem
{

    public float rateDecay = 0.2f;
    public float growthRate = 0.1f;
    public float JourneyStart = 0.001f;
    private float i = 0;

    protected override void Update()
    {
        /*if (rb_Obj.isKinematic == true)
        {
            i += Time.deltaTime;
        }*/

        CalcDistAndDir();

        if (reachedTarget == true)
        {
            //Debug.Log("Reached Target");
            TargetReached();
        }
        else if ( Mathf.Abs(rb_Obj.velocity.magnitude) >= Mathf.Abs((direction.normalized * speed).magnitude))
        {
            rb_Obj.velocity = direction.normalized * speed;
            JourneyStart = -1f;
            //Debug.Log("Caught");
        }
        else if(JourneyStart != -1f)
        {
            if ( Mathf.Abs(rb_Obj.velocity.magnitude) < 0.0001f)
            {
                rb_Obj.velocity = direction * Mathf.Pow((1 + growthRate), Time.time - JourneyStart);
                //Debug.Log("Init");
            }
            else
            {
                //Debug.Log("Normal Path");
                rb_Obj.velocity = rb_Obj.velocity * Mathf.Pow((1 + growthRate), Time.time - JourneyStart);
            }
        }
        else if(Mathf.Abs(rb_Obj.velocity.magnitude) < 0.001)
        {
            rb_Obj.velocity = direction.normalized * speed;
        }

       // Debug.Log(rb_Obj.velocity);
    }


    public override void TargetReached()
    {
        rb_Obj.velocity = rb_Obj.velocity * Mathf.Pow((1 - rateDecay), Time.time - timeReached);

        if (rb_Obj.velocity.magnitude < 0.0001f)
        {
            rb_Obj.velocity = new Vector3(0f, 0f, 0f);
            reachedTarget = false;
            JourneyStart = Time.time;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(pauseTime);
    }

    /*rb_Obj.isKinematic = true;
        i += Time.deltaTime;
        Debug.Log(i >= pauseTime);

        if (i >= pauseTime)
        {
            rb_Obj.isKinematic = false;
            if (movingObj.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                reachedTarget = false;
                i = 0;
            }
        }*/
}