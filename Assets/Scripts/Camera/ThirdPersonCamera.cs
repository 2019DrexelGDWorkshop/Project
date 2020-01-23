using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 lookOffset;

    Vector3 offset;

    public float rotateX;
    public float rotateY;

    public void Init()
    {
        offset = transform.position - (target.position + lookOffset);
    }

    // Update is called once per frame
    public void Movement()
    {
        transform.position = target.position + offset;
        Rotate();
    }

    public void Rotate()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;

        transform.RotateAround(target.position, Vector3.up, rotateX * 10);
        //transform.RotateAround(target.position, Vector3.left, -rotateY * 10);

        /*float x = transform.eulerAngles.x;
        float y = transform.eulerAngles.y;

        if (x < 20 || x > 45 || y < 0 || y > 40)
        {
            transform.position = pos;
            transform.eulerAngles = rot;
        }*/

        offset = transform.position - target.position;
    }
}
