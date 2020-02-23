using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject fallingObject;
    public int delayTime=5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            yield return new WaitForSeconds(delayTime);
            fallingObject.GetComponent<Rigidbody>().useGravity = true;

        }
    }
    

}
