using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Coll_and_Trig : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered A Trigger");
        if (other.tag == "obj_anchor")
        {
            //Debug.Log(other.name);
            this.transform.parent.GetComponent<PatrolSystem>().Next();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.gameObject.GetComponent<CharacterMovement>().Kill();
        }
    }
}
