﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueElders : MonoBehaviour
{
    public GameObject DialogueManager;
    public GameObject DialogueText;
    public GameObject ContinueKey;
    int indexNum;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            //gameObject.GetComponent<CircleCollider2D>().radius = 15;
            DialogueManager.GetComponent<ElderSpiritsDialogue>().enabled = true;
            DialogueText.SetActive(true);
            ContinueKey.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            //gameObject.GetComponent<CircleCollider2D>().radius = 9;
            DialogueManager.GetComponent<ElderSpiritsDialogue>().enabled = false;
            DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
        }
    }
}