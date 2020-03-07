using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuTop : MonoBehaviour
{
    public GameObject headO;
    public GameObject bodyO;
    public GameObject buttonX;

    public void KillOyuTop()
    {
        Destroy(buttonX);
        headO.GetComponent<OyuBehv>().KillOyu();
        bodyO.GetComponent<OyuBehv>().KillOyu();
    }
}
