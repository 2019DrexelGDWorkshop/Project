using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MoveAndAnchorSystem
{
    public bool canJump;

    public Transform edgeCheckSource;

    public GameObject player = null;

    public int jumpForce;

    public int gravity;

    protected float distToGround;

    float jumpStart;

    protected override void Start()
    {
        movingObj.transform.position = anchor[0].transform.position;

        foreach (GameObject obj in anchor)
        {
            obj.SetActive(false);
        }

        anchor[targetNumb].SetActive(true);

        player = LevelManager.Instance.player;
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

        rb_Obj.velocity = speed * movingObj.transform.forward;


        if( canJump && !(Physics.Raycast(edgeCheckSource.position, Vector3.down, 10)) && Physics.Raycast(movingObj.transform.position, Vector3.down, 1.5f))
        {
            jumpStart = Time.realtimeSinceStartup;
            movingObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10 * jumpForce, 0));
            
        }

        if ( canJump && !(Physics.Raycast(movingObj.transform.position, Vector3.down, 1.5f)))
        {
            float timeDif = Time.realtimeSinceStartup - jumpStart;
            //print(timeDif);
            movingObj.GetComponent<Rigidbody>().AddForce(new Vector3(0,  -(gravity * timeDif ), 0));
        }
        else
        {
            movingObj.GetComponent<Rigidbody>().velocity = new Vector3(movingObj.GetComponent<Rigidbody>().velocity.x, -(gravity * 0.1f ), movingObj.GetComponent<Rigidbody>().velocity.z);
        }
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
