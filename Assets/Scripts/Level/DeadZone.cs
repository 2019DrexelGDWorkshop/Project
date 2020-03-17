using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    public GameObject DEGO;
    public Transform startPos;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audioSource.Play();
            DEGO.GetComponent<DeathEffectManager>().DeathEffect();
            other.gameObject.transform.position = LevelManager.Instance.lastCheckPoint.position;
        }
    } 
}
