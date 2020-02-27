using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuTop : MonoBehaviour
{
    public GameObject headO;
    public GameObject bodyO;

    public void KillOyuTop()
    {
        headO.GetComponent<OyuBehv>().KillOyu();
        bodyO.GetComponent<OyuBehv>().KillOyu();
    }
}
