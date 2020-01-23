using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxLiarScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] listJump;
    public AudioClip[] listWhoosh;
    private AudioClip sfxClip;

    private int rewiredPlayerID = 0; //TODO: This should be housed somewhere I can grab it, like GM.
    private Rewired.Player rewiredPlayer; //TODO: Above applies here too.

    // Start is called before the first frame update
    void Start()
    {
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(rewiredPlayerID);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.Jump))//Input.GetKeyDown(KeyCode.Space))
        {
            int index = Random.Range(0, listJump.Length);
            sfxClip = listJump[index];
            audioSource.clip = sfxClip;
            audioSource.Play();
        }
        if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.PerspectiveSwitch))//Input.GetMouseButtonDown(0))
        {
            int index = Random.Range(0, listWhoosh.Length);
            sfxClip = listWhoosh[index];
            audioSource.clip = sfxClip;
            audioSource.Play();
        }
    }
}