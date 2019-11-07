using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTest : MonoBehaviour
{
    public Transform[] links;
    //public Rigidbody thisOnesrb;
    const float dst = .5f;

    void Update()
    {
        Vector3 prevPoint = transform.position;
        foreach (Transform link in links)
        {
            Vector3 dir = (prevPoint - link.position).normalized;
            link.position = prevPoint - dir * dst;
            prevPoint = link.position;
        }
    }
}
