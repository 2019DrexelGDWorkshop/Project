using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnchor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found Trigger");
        if (other.tag == "obj_anchor")
        {
            Debug.Log("Found Anchor");
            this.transform.parent.GetComponent<PatrolSystem>().Next();
        }
    }
}
