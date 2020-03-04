using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyboxMath : MonoBehaviour
{
    void LateUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", (Time.time * 1.5f));
    }
}
