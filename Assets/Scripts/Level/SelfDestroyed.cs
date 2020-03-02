using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyed : MonoBehaviour
{
    public float timeToLive=7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
