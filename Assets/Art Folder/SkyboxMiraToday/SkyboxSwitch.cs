using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSwitch : MonoBehaviour
{
    public float expFl;
    public float incroFl = .01f;
    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*2);
        RenderSettings.skybox.SetFloat("_Exposure", expFl);

        if ((Time.time) % 10f <= 6)
        {
            expFl = expFl + (incroFl/2);
        }
        else
        {
            expFl = expFl - incroFl;
        }
        counter++;

        if (counter >= 600)
        {
            counter = 0;
            expFl = 0;
        }
    }
}
