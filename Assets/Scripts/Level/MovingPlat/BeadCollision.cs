using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadCollision : MonoBehaviour
{
    public bool isEnabled;

    private void OnCollisionEnter(Collision collision)
    {
        if ( isEnabled && (collision.collider.name == "Ab_Edge_Top" || collision.collider.tag == "Ab_Bead"))
        {
            this.transform.parent.GetComponent<Moving_Ab_Beads>().nextBead();

        }
    }
}
