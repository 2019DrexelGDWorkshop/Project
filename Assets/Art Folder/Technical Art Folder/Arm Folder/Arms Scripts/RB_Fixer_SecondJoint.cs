using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Fixer_SecondJoint : MonoBehaviour
{
    public Transform jointBeforeGameObject;
    //public Transform thisGOtransform;
    public GameObject thisGameObject;
    public Rigidbody rBody;
    public bool RBsOnOrOff;

    void Start()
    {

        Rigidbody rBody = thisGameObject.AddComponent<Rigidbody>();

    }




    void Update()
    {
        if(thisGameObject.transform.position.z <= (jointBeforeGameObject.transform.position.z + .008f))
        {
            thisGameObject.transform.position = new Vector3(thisGameObject.transform.position.x, thisGameObject.transform.position.y, jointBeforeGameObject.transform.position.z);
            Destroy(thisGameObject.GetComponent<Rigidbody>());
            RBsOnOrOff = true;

        }



        if ((thisGameObject.transform.position.z == jointBeforeGameObject.transform.position.z) && (thisGameObject.transform.position.x == jointBeforeGameObject.transform.position.x))
        {

        }
        else
        {
            RBsOnOrOff = false;
            Rigidbody rBody = thisGameObject.AddComponent<Rigidbody>();
            //print("else");
        }
    }
}
