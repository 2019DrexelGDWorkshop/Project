using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Fixer : MonoBehaviour
{
    public Transform jointBeforeGameObject;
    //public Transform thisGOtransform;
    public GameObject thisGameObject;
    public Rigidbody rBody;
    public GameObject secondJoint;

    void Start()
    {

        Rigidbody rBody = thisGameObject.AddComponent<Rigidbody>();

    }
    



    void Update()
    {

        bool onOrOffbool = GameObject.Find("armPart2").GetComponent<RB_Fixer_SecondJoint>().RBsOnOrOff;


        if (onOrOffbool)
        {

            Destroy(thisGameObject.GetComponent<Rigidbody>());
        }
        else
        {
            Rigidbody rBody = thisGameObject.AddComponent<Rigidbody>();
        }
    }
}
