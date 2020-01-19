using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovingPlat : MonoBehaviour
{

    public GameObject[] anchor;

    public GameObject movingPlat;

    public int speed;

    public float rateDecay = 0.9f;

    private bool reachedTarget = false;

    private int targetNumb = 1;

    private float distance;

    private Vector3 direction;

    private float startSlow;


    // Start is called before the first frame update
    void Start()
    {
        movingPlat.transform.position = anchor[0].transform.position;

        foreach (GameObject obj in anchor){
            obj.SetActive(false);
        }

        anchor[targetNumb].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = anchor[targetNumb].transform.position - movingPlat.transform.position;
        distance = heading.magnitude;
        direction = heading/distance;

      

        Rigidbody rb_mPlat = movingPlat.GetComponent<Rigidbody>();
        if(rb_mPlat.velocity.magnitude > speed)
        {
            rb_mPlat.velocity = rb_mPlat.velocity.normalized * speed;
        }
        else if (reachedTarget)
        {
            movingPlat.GetComponent<Rigidbody>().velocity = movingPlat.GetComponent<Rigidbody>().velocity * Mathf.Pow((1-rateDecay), Time.time - startSlow);
            if(movingPlat.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                reachedTarget = false;
            }
        }
        else
        {
            rb_mPlat.AddForce(speed * direction);
        }
        
    }

    public void Next()
    {
        reachedTarget = true;
        startSlow = Time.time;
        anchor[targetNumb].SetActive(false);
        if (targetNumb == anchor.Length -1)
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
