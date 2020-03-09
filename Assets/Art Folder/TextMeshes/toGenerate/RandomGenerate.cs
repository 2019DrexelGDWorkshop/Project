using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerate : MonoBehaviour
{
    public Transform gennerGO;
    public GameObject[] mathsSymbols;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(mathsSymbols[Random.Range(0, mathsSymbols.Length)], gennerGO.transform.position, gennerGO.transform.rotation);
        //Destroy(this);
    }
}
