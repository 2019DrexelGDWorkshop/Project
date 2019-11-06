using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClamper : MonoBehaviour
{
    public Transform[] links;




    void Update()
    {

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, links[0].transform.position);
        lineRenderer.SetPosition(1, links[1].transform.position);
        lineRenderer.SetPosition(2, links[2].transform.position);
        lineRenderer.SetPosition(3, links[3].transform.position);
    }
}
