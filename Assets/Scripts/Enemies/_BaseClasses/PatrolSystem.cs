using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MoveAndAnchorSystem
{
    public Transform edgeCheckSource;

    public GameObject player = null;

    public int jumpForce;

    public int gravity;

    protected override void Start()
    {
        movingObj.transform.position = anchor[0].transform.position;

        foreach (GameObject obj in anchor)
        {
            obj.SetActive(false);
        }

        anchor[targetNumb].SetActive(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        CalcDistAndDir();
        Rigidbody rb_Obj = movingObj.GetComponent<Rigidbody>();
       
        if (player)
        {
            Vector3 target = new Vector3(player.transform.position.x, movingObj.transform.position.y, player.transform.position.z);
            
            movingObj.transform.LookAt(target, movingObj.transform.up);
        }
        else
        {
            Vector3 target = new Vector3(anchor[targetNumb].transform.position.x, movingObj.transform.position.y, anchor[targetNumb].transform.position.z);
            movingObj.transform.LookAt(target, movingObj.transform.up);
        }

        if( !(Physics.Raycast(edgeCheckSource.position, transform.TransformDirection(Vector3.down), 10)) && Physics.Raycast(movingObj.transform.position, transform.TransformDirection(Vector3.down)))
        {
            print("AI Jumping");
            movingObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10 * jumpForce, 0));
        }

        if ( !(Physics.Raycast(movingObj.transform.position, transform.TransformDirection(Vector3.down), 2)))
        {
            print("AI Falling");
            movingObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, movingObj.GetComponent<Rigidbody>().velocity.y - (gravity * Time.time), 0));
        }


        /* else if (!isjumping && !IsGrounded())
        moveVec.y = tmpy - gravity * Time.deltaTime; */

        rb_Obj.velocity = speed * movingObj.transform.forward;


    }

    public override void CalcDistAndDir()
    {
        if (player)
        {
            Vector3 heading = player.transform.position - movingObj.transform.position;
            distance = heading.magnitude;
            direction = heading / distance;
        }
        else
        {
            Vector3 heading = anchor[targetNumb].transform.position - movingObj.transform.position;
            distance = heading.magnitude;
            direction = heading / distance;
        }
    }

    public void playerFound(GameObject target)
    {
        player = target;
    }

   
}
