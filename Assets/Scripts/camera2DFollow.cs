using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera2DFollow : MonoBehaviour
{

    [SerializeField]
    public Transform followTarget = null;

    public Vector3 moveDirection = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = followTarget.position + offset;
        
        
            Vector3 tmp = followTarget.position - transform.position;
            transform.position += new Vector3(tmp.x * moveDirection.x, tmp.y * moveDirection.y, tmp.z * moveDirection.z);

        
    }

}
