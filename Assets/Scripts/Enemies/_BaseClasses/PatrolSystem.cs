using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MoveAndAnchorSystem
{

    public Transform edgeCheckSource;

    public GameObject player = null;

    public int gravity;

    private Vector3 startPos;

    protected override void Start()
    {
        movingObj.transform.position = anchor[0].transform.position;

        foreach (GameObject obj in anchor)
        {
            obj.SetActive(false);
        }

        anchor[targetNumb].SetActive(true);

        startPos = movingObj.transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        float distance = 0f;
        Vector3 direction = new Vector3(0, 0, 0);
        CalcDistAndDir(ref distance, ref direction);
        Rigidbody rb_Obj = movingObj.GetComponent<Rigidbody>();
       
        if (player)
        {
            Vector3 target = new Vector3(player.transform.position.x, movingObj.transform.position.y, player.transform.position.z);
            movingObj.transform.GetChild(0).transform.LookAt(target, movingObj.transform.up);
        }
        else
        {
            Vector3 target = new Vector3(anchor[targetNumb].transform.position.x, movingObj.transform.position.y, anchor[targetNumb].transform.position.z);
            movingObj.transform.GetChild(0).transform.LookAt(target, movingObj.transform.up);
        }


        rb_Obj.velocity = speed * direction;

        if (!(Physics.Raycast(edgeCheckSource.position, Vector3.down, 10)))
        {
            StartCoroutine("WaitAndReset");
        }
        else
        {
            movingObj.GetComponent<Rigidbody>().velocity = new Vector3(movingObj.GetComponent<Rigidbody>().velocity.x, -(gravity * 0.1f ), movingObj.GetComponent<Rigidbody>().velocity.z);
        }
    }

    public void CalcDistAndDir(ref float distance, ref Vector3 direction)
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


    public void Reset()
    {
        movingObj.transform.position = startPos;
        player = null;
    }

    IEnumerator WaitAndReset()
    {
        movingObj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        yield return new WaitForSecondsRealtime(2);
        player = null;
        StopCoroutine("WaitAndReset");
    }

}
