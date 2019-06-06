using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    //The DialogueManager contains all components and information regarding the dialogue
    public GameObject DialogueManager;
    //TextMeshPro object handling the written text's appearance.
    public GameObject DialogueText;
    //UI showing 'Press B' 
    public GameObject ContinueKey;
    //Spirit's animator
    public Animator SpiritAnim;

    //Timer variables used for the delay needed after the first encounter with the spirit.
    //During this short delay, everything is disabled, in order to avoid the continuation
    //of text display until the Player has reached the second checkpoint.
    float timer = 0;
    float timeToWait = 1f;

    // Update is called once per frame

    private void Update()
    {
        //Index of sentences on the DialogueManager
        //For each sentence, a sprite animation is played.
        if (DialogueManager.GetComponent<Dialogue>().index == 0)
        {
            SpiritAnim.Play("WaterSpirit_Idle");
        }

        if (DialogueManager.GetComponent<Dialogue>().index == 1)
        {
            SpiritAnim.Play("WaterSpirit_Panic");
        }

        if (DialogueManager.GetComponent<Dialogue>().index == 2)
        {
            SpiritAnim.Play("WaterSpirit_Fly");
        }

        if (DialogueManager.GetComponent<Dialogue>().index == 3)
        {
            SpiritAnim.Play("WaterSpirit_FlyIdle");
        }
        //Disable() is called to ensure that the last sentence of the spirit's index is not being played
        //before it reaches the second checkpoint 
        if (DialogueManager.GetComponent<Dialogue>().index == 4)
        {
            Disable();
            SpiritAnim.Play("WaterSpirit_FlyOff");
        }

        //After the last sentence has been written out, all components related to the spirit's dialogue system
        //are being disabled/set to false. What is left is merely a non-interactable floating spirit.
        if (DialogueManager.GetComponent<Dialogue>().index == 6)
        {
            SpiritAnim.Play("WaterSpirit_FlyOff2");
            ContinueKey.SetActive(false);
            DialogueManager.GetComponent<Dialogue>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().radius = 1;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }

    }
    //Checking if the object tagged "Player" has entered or exited the spirit's CircleCollider
    //Which enables or disables the attached components and variables. 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            DialogueText.SetActive(true);
            ContinueKey.SetActive(true);
            DialogueManager.GetComponent<Dialogue>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().radius = 15;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 5;
            DialogueManager.GetComponent<Dialogue>().enabled = false;
            DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
        }
    }

    //Called when the last sentence of the first encounter with the spirit is finished.
    void Disable()
    {
        //Spirit's collider
        Collider2D col = gameObject.GetComponent<CircleCollider2D>();

        //Running timer 
        timer += Time.deltaTime;

        //If the timer has not reached the pre-defined time count, disable all objects 
        //to avoid the last sentence to be played. This is being done during the time
        //that the spirit's flies off to the 2nd checkpoint.
        if (timer <= timeToWait)
        {
            ContinueKey.SetActive(false);
            DialogueText.SetActive(false);
            DialogueManager.GetComponent<Dialogue>().enabled = false;
        }
        //If the timer has reached the pre-defined time count AND if Player has trigger-entered the spirit's collider
        //enable everything to allow the player to activate the last sentence of the index.
        if (timer >= timeToWait)
        {

            if (col.tag == "Player")
            {
                DialogueText.SetActive(true);
                ContinueKey.SetActive(true);
                DialogueManager.GetComponent<Dialogue>().enabled = true;
                gameObject.GetComponent<CircleCollider2D>().radius = 15;
            }

        }

    }

}
