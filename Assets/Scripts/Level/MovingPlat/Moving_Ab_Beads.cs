using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Ab_Beads : MonoBehaviour
{

    public GameObject[] beads;

    public int numberToLift;

    public GameObject Ab_Edge_Bottom;

    public GameObject Ab_Edge_Top;

    public double speed;

    protected GameObject currentBead;

    public int beadsMoved = 0;

    public bool allRaised = false;

    public float timeToWait = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        currentBead = beads[0];
        currentBead.GetComponent<Rigidbody>().useGravity = false;
        currentBead.GetComponent<BeadCollision>().isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (beadsMoved < numberToLift)
        {
            currentBead.GetComponent<Rigidbody>().velocity = new Vector3(0, ((float)speed * 10f), 0);
        }

        if(allRaised && beadsMoved == numberToLift)
        {
            StartCoroutine("ResetBeads");
        }
        
    }

    public void nextBead()
    {
        Debug.Log("NextBead");
  
        currentBead.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        currentBead.GetComponent<Rigidbody>().isKinematic = true;
        currentBead.GetComponent<BeadCollision>().isEnabled = false;
        beadsMoved += 1;

        if (beadsMoved < numberToLift )
        {
            Debug.Log("still proccessing");
            currentBead = beads[beadsMoved];
            currentBead.GetComponent<Rigidbody>().isKinematic = false;
            currentBead.GetComponent<BeadCollision>().isEnabled = true;
            currentBead.GetComponent<Rigidbody>().useGravity = false;
        }
        if(beadsMoved == numberToLift)
        {
            Debug.Log("total reached");
             for ( int i = 0; i < numberToLift; i++)
             {
                 Debug.Log(i);
                 beads[i].GetComponent<Rigidbody>().isKinematic = false;
                 beads[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                 beads[i].GetComponent<Rigidbody>().useGravity = true;
                 beads[i].GetComponent<BeadCollision>().isEnabled = false;
            }
            allRaised = true;
        }
    }

    IEnumerator ResetBeads()
    {
        yield return new WaitForSeconds(timeToWait);

        beadsMoved = 0;
        currentBead = beads[beadsMoved];
        currentBead.GetComponent<Rigidbody>().useGravity = false;
        currentBead.GetComponent<BeadCollision>().isEnabled = true;
        allRaised = false;

        StopCoroutine("ResetBeads");
    }

}
