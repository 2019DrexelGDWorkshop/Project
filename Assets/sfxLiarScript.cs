using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxLiarScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] listJump;
    public AudioClip[] listWhoosh;
    private AudioClip sfxClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = Random.Range(0, listJump.Length);
            sfxClip = listJump[index];
            audioSource.clip = sfxClip;
            audioSource.Play();
        }
        if (Input.GetMouseButtonDown(0))
        {
            int index = Random.Range(0, listWhoosh.Length);
            sfxClip = listWhoosh[index];
            audioSource.clip = sfxClip;
            audioSource.Play();
        }
    }
}