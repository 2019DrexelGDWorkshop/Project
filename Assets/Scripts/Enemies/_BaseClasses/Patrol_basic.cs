using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_basic : Enemy_base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider _col)
    {

    }

    private void OnCollision(Collision collision)
    {
        print("collisionWith " + collision.collider.transform.name);
        if (collision.collider.tag == "player")
        {
            Destroy(this.gameObject, 5);
        }
    }
}

