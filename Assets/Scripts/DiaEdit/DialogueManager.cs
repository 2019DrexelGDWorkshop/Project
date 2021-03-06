﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public AudioSource AudioSource;
    public int i = 0;
    public AudioSource OyuVoiceLine;
    //public AudioSource nextVoiceLine;
    public Text nameText;
    public Text dialogueText;
    public bool inChat = false;
    public GameObject DiaUIobj;
    public GameObject Oyu;

    //public Animator animator;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (inChat)
        {
            if (!FindObjectOfType<NPCBehavior>().inRange)
            {
                EndDialogue();
            }
        }
        //DiaUIobj.SetActive(inChat);
    }
    public void StartDialogue (Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true);
        OyuVoiceLine.Play();
        inChat = true;
        DiaUIobj.SetActive(inChat);
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        //nextVoiceLine = OyuVoiceLines[i];
        //i++;
        

        if (sentences.Count == 1)
        {
            Debug.Log("ummmm");
            EndDialogue();
            return;
        }
        //nextVoiceLine.Play();

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);wa
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
        inChat = false;
        DiaUIobj.SetActive(inChat);
        Oyu.GetComponent<OyuTop>().KillOyuTop();
        //Debug.Log("End of conversation.");
        //Destroy(Oyu);
    }
}
