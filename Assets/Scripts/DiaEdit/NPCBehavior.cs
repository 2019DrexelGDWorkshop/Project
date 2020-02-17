using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Dialogue dialogue;
    public bool inRange = false;
    public bool chatStarted = false;
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
        if (Input.GetKeyDown("j") && inRange && !chatStarted) //change input to correct script
        {
            chatStarted = true;
            TriggerDialogue();
        }
        else if (Input.GetKeyDown("j") && chatStarted)
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
