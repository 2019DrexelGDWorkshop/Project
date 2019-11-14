using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    [SerializeField]
    float VerticalMax;

    [SerializeField]
    float VerticalMin;

    [SerializeField]
    float platSpeed;

    [SerializeField]
    bool movingUp;

    Transform plat;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        movingUp = true;
        plat = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plat.position.y >= VerticalMax)
        {
            movingUp = false;
            print(plat.position.y);
        }
        else if (plat.position.y <= VerticalMin)
        {
            movingUp = true;
            print(plat.position.y);
        }

        if (movingUp && rb.velocity.y <= platSpeed )
        {
            //plat.position = new Vector3(plat.position.x, VerticalMax, plat.position.z);
            print("Added Up Force");
            rb.AddForce(new Vector3(0, 2.0f, 0));
        }
        else if (!movingUp && rb.velocity.y >= -1*platSpeed )
        {
            //plat.position = new Vector3(plat.position.x, VerticalMin, plat.position.z);
            print("Added Down Force");
            rb.AddForce(new Vector3(0, -2.0f, 0));
        }
    }
}
