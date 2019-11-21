using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodedCloudAnimationsScript : MonoBehaviour
{
    public float animSpeed;
    public int onOrnot;
    public float startingPos;
    //public float Yticker;
    public bool goBack;

    void Awake()
    {
        startingPos = transform.position.y;
        onOrnot = Random.Range(0, 2);
        if (onOrnot == 0)
        {
            animSpeed = Random.Range(0.3f, 1.5f);
        }
        if (onOrnot == 1)
        {
            animSpeed = Random.Range(0.1f, .5f);
        }
        if (onOrnot == 2)
        {
            animSpeed = 0f;
        }

    }

    void FixedUpdate()
    {
            UpDown();
    }

    public void UpDown()
    {
        //transform.position += new Vector3 (0, (.01f * animSpeed),0);
        if (transform.position.y > startingPos + 1f)
        {
            goBack = true;   
        }
        if(transform.position.y < startingPos - 1f){
            goBack = false;
        }
        if (goBack)
        {
            transform.position -= new Vector3(0, (.01f * animSpeed), 0);
        }
        if (!goBack)
        {
            transform.position += new Vector3(0, (.01f * animSpeed), 0);
        }
    }


    
            
}


//transform.position
//use local space
//up and down
//down and up
//.y only