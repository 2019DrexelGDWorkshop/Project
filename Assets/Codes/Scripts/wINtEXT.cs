using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wINtEXT : MonoBehaviour
{
    public GameObject winText;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        winText.SetActive(true);
    }
}
