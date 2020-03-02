using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MoveAndAnchorSystem
{

    public float rateDecay = 0.2f;
    private float i = 0;

    protected void FixedUpdate()
    {
        if (rb_Obj.isKinematic == true)
        {
            i += Time.deltaTime;
        }
        
        CalcDistAndDir();

        
        


        if (rb_Obj.velocity.magnitude > speed)
        {
                rb_Obj.velocity = rb_Obj.velocity.normalized * speed;
        }

        else if (reachedTarget)
        {
            
            TargetReached();    
        }
       
        else
        { 
            rb_Obj.AddForce(speed * direction);
        }
    }


    public override void TargetReached()
    {
        movingObj.GetComponent<Rigidbody>().velocity = movingObj.GetComponent<Rigidbody>().velocity * Mathf.Pow((1 - rateDecay), Time.time - timeReached);
        //print(movingObj.GetComponent<Rigidbody>().velocity);
        rb_Obj.isKinematic = true;
        i += Time.deltaTime;
        if (i >= pauseTime)
        {
            rb_Obj.isKinematic = false;
            if (movingObj.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                reachedTarget = false;
                i = 0;
            }
        }

        
            

        

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(pauseTime);
    }
}
