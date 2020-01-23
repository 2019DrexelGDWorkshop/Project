using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{ 
    void Awake()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        float totalDuration = particleSystem.duration + particleSystem.startLifetime;
        Destroy(this.gameObject, totalDuration);
    }
}
