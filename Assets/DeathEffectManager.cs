using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectManager : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathEffect()
    {
        cam1.GetComponent<PixelateImageEffect>().enabled = true;
        cam2.GetComponent<PixelateImageEffect>().enabled = true;
    }



}
