using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndAnchorSystem : MonoBehaviour
{

    public GameObject[] anchor;

    public GameObject movingObj;

    public int speed;

    protected bool reachedTarget = false;

    protected float timeReached;

    protected int targetNumb = 1;

    protected float distance;

    protected Vector3 direction;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        movingObj.transform.position = anchor[0].transform.position;

        foreach (GameObject obj in anchor)
        {
            obj.SetActive(false);
        }

        anchor[targetNumb].SetActive(true);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalcDistAndDir();
        Rigidbody rb_Obj = movingObj.GetComponent<Rigidbody>();
        if (rb_Obj.velocity.magnitude > speed)
        {
            rb_Obj.velocity = rb_Obj.velocity.normalized * speed;
        }
        else
        {
            rb_Obj.AddForce(speed * direction);
        }
    }

    public virtual void CalcDistAndDir()
    {
        Vector3 heading = anchor[targetNumb].transform.position - movingObj.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;
    }

    public virtual void TargetReached()
    {
 
    }

    public virtual void Next()
    {
        timeReached = Time.time;
        reachedTarget = true;
        anchor[targetNumb].SetActive(false);
        if (targetNumb == anchor.Length - 1)
        {
            targetNumb = 0;
        }
        else
        {
            targetNumb++;
        }
        anchor[targetNumb].SetActive(true);
    }
}
