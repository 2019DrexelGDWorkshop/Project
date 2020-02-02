using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    Rigidbody rb;
    CharacterMovement charMove;
    bool shouldAdd = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (charMove == null)
            {
                charMove = other.gameObject.GetComponent<CharacterMovement>();
            }
            shouldAdd = true;
            
            //other.transform.SetParent(this.transform);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (charMove == null)
            {
                charMove = other.gameObject.GetComponent<CharacterMovement>();
            }
            shouldAdd = false;
            //other.gameObject.GetComponent<CharacterMovement>().externalForces -= this.GetComponent<Rigidbody>().velocity;
            //other.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if(shouldAdd)
        {
            charMove.externalForces += rb.velocity;
        }
    }
}
