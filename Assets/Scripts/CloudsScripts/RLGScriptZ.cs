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
    public float lowScale1 = 1f;
    public float lowScale2 = 1f;
    public float lowScale3 = 1f;
    public float lowScale4 = 1f;
    public float lowScale5 = 1f;
    public float lowScale6 = 1f;
    public float lowSx;
    public float lowSy;
    public float lowSz;

    void Start()
    {
        foreach (Transform j in LineStarters)
        {
            for (int i = 0; i < AmountOfInstantiations; i++)
            {
                var cloud = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], j.transform.position, j.transform.rotation);
                j.transform.position = j.transform.position + new Vector3(0, 0, Random.Range(lowMinusZ, highMinusZ));
                //cloud.transform.localScale = cloud.transform.localScale * 2;
                cloud.transform.parent = cloudMaster.transform;
                lowSx = Random.Range(lowScale1, lowScale2);
                lowSy = Random.Range(lowScale3, lowScale4);
                lowSz = Random.Range(lowScale5, lowScale6);
                cloud.transform.localScale += new Vector3(lowSx, lowSy, lowSz);
                cloud.GetComponent<Renderer>().material = MatsForPrefabs[Random.Range(0, MatsForPrefabs.Length)];
                cloud.GetComponent<Renderer>().receiveShadows = false;
            }
            
        }
    }
}
