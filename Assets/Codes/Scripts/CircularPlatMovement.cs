using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatMovement : MonoBehaviour
{
    [SerializeField]
    Transform center;

    [SerializeField]
    float radius = 2f;

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    float angle = 360f;

    float zPos;

    float yPos;


    // Update is called once per frame
    void Update()
    {
        zPos = center.position.z + Mathf.Cos(angle) * radius;
        yPos = center.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(transform.position.x, yPos, zPos);
        angle = angle - Time.deltaTime * speed;

        if ( angle <= 0f)
        {
            angle = 360f;
        }
    }
}
