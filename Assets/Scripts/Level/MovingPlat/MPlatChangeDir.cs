using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlatChangeDir : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="mplat_anchor")
        {
            this.transform.parent.GetComponent<LineMovingPlat>().Next();
        }
    }
}
