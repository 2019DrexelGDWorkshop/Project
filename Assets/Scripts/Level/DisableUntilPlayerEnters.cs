using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUntilPlayerEnters : MonoBehaviour
{
    public GameObject movingPlatform;

    void Start()
    {
        movingPlatform.GetComponent<PlatformMoving>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            movingPlatform.GetComponent<PlatformMoving>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
