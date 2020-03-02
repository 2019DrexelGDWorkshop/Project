using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class NPCBehavior : MonoBehaviour
{
    public Dialogue dialogue;
    public bool inRange = false;
    public bool chatStarted = false;

    private Player rewiredPlayer;

    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    public void OnTriggerEnter(Collider other) //set up sphere collider with preferred limit
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    public void Update()
    {
        if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.Dialogue) && inRange && !chatStarted) //Input.GetKeyDown("j") && inRange && !chatStarted) //change input to correct script
        {
            chatStarted = true;
            TriggerDialogue();
        }
        else if (rewiredPlayer.GetButtonDown(RewiredConsts.Action.Dialogue) && chatStarted)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log("TD triggered!");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
