﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlatChangeDir : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="obj_anchor")
        {
            this.transform.parent.GetComponent<PlatformMoving>().Next();
        }
    }
}
