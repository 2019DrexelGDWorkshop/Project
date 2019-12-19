using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMultiplier : MonoBehaviour
{
    public Vector3 velGotten;
    public float vPAbs;
    public float FTval;
    public GameObject thingWithCMscriptOnIt;
    private CharacterMovement theScript;
    public Animator thisAnim;
    public GameObject thisGO;

    

    void LateUpdate()
    {
        thisAnim = thisGO.GetComponent<Animator>();
        theScript = thingWithCMscriptOnIt.GetComponent<CharacterMovement>();
        //Debug.Log(theScript.moveVec);
        velGotten = theScript.moveVec;
        vPAbs = (Math.Abs(velGotten.x) + Math.Abs(velGotten.y) + Math.Abs(velGotten.z)) * FTval;
        thisAnim.speed = vPAbs;
    }
}
