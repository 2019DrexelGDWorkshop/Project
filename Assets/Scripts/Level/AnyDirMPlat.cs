using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirMPlat : MonoBehaviour
{
    public enum Axis { X, Y, Z};

    public Axis moveAxis;

    public float maxCoord;

    public float minCoord;

    public float velocity;

    public float speedInc;

    private int axisInt;



    public 

    // Start is called before the first frame update
    void Start()
    {
        Vector3 force;

        axisInt = (int)moveAxis;

        switch (axisInt)
        {
            case 0:
                force = new Vector3(velocity, 0f, 0f);
                break;
            case 1:
                force = new Vector3(0f, velocity, 0f);
                break;
            case 2:
                force = new Vector3(0f, 0f, velocity);
                break;
            default:
                force = new Vector3(0f, 0f, 0f);
                break;
        }
        print(force);

        this.gameObject.GetComponent<Rigidbody>().AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 addForce = new Vector3(0f, 0f, 0f);

        switch (axisInt)
        {
            case 0:
                if(this.gameObject.transform.position.x > maxCoord && this.gameObject.GetComponent<Rigidbody>().velocity.x >= -velocity)
                {
                    addForce = new Vector3(-speedInc, 0f, 0f);
                }
                else if (this.gameObject.transform.position.x < minCoord && this.gameObject.GetComponent<Rigidbody>().velocity.x <= velocity)
                {
                    addForce = new Vector3(speedInc, 0f, 0f);
                }
                break;
            case 1:
                if (this.gameObject.transform.position.y > maxCoord && this.gameObject.GetComponent<Rigidbody>().velocity.y >= -velocity)
                {
                    addForce = new Vector3( 0f, -speedInc, 0f);
                }
                else if (this.gameObject.transform.position.y < minCoord && this.gameObject.GetComponent<Rigidbody>().velocity.y <= velocity)
                {
                    addForce = new Vector3(0f, speedInc, 0f);
                }
                break;
            case 2:
                if (this.gameObject.transform.position.z > maxCoord && this.gameObject.GetComponent<Rigidbody>().velocity.z >= -velocity)
                {
                    addForce = new Vector3(0f, 0f, -speedInc);
                }
                else if (this.gameObject.transform.position.z < minCoord && this.gameObject.GetComponent<Rigidbody>().velocity.z <= velocity)
                {
                    addForce = new Vector3(0f, 0f, speedInc);
                }
                break;
            default:
                addForce = new Vector3(0f, 0f, 0f);
                print("MovePlat Broke Adding Forces");
                break;
        }

        this.gameObject.GetComponent<Rigidbody>().AddForce(addForce);
    }
}
