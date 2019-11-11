using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLGScriptZ : MonoBehaviour
{
    public Transform[] LineStarters;
    public GameObject[] Prefabs;
    public Material[] MatsForPrefabs;
    public GameObject cloudMaster;
    public int AmountOfInstantiations = 5;
    public float lowMinusZ = .1f;
    public float highMinusZ= 2f;

    void Start()
    {
        foreach (Transform j in LineStarters)
        {
            for (int i = 0; i < AmountOfInstantiations; i++)
            {
                var cloud = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], j.transform.position, j.transform.rotation);
                j.transform.position = j.transform.position + new Vector3(0, 0, Random.Range(lowMinusZ, highMinusZ));
                cloud.transform.parent = cloudMaster.transform;
                cloud.GetComponent<Renderer>().material = MatsForPrefabs[Random.Range(0, MatsForPrefabs.Length)];
                cloud.GetComponent<Renderer>().receiveShadows = false;
            }
            
        }
    }
}
