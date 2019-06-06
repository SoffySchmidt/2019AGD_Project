using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Handle's text and its output
    public TextMeshProUGUI textDisplay;
    //string array of sentences, written in the inspector of the DialogueManager
    public string[] sentences;
    //Index number of sentence(s)
    public int index;
    //The speed of which the text is being written out
    public float typingSpeed;

    //Animator for the TextDisplay: Handles the fade-in animation which is being played
    //every time a new sentence is shown
    public Animator textDisplayAnim;
    //UI object for the display of Continue Key, which is merely a sprite and a text saying "Press B"
    public GameObject continueKey;


    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        //If the displayed sentence of the given index number X is written out in its full length,
        //set animation bool "Change" to false (i.e. to stay idling instead) and display the text "Press B"
        //to show the player that it is now possible to proceed to the next step
        if (textDisplay.text == sentences[index])
        {
            textDisplayAnim.SetBool("Change", false);
            continueKey.SetActive(true);

            //if the continueKey text is displayed AND the input-button called "Text" has been pressed
            //Call the method NextSentence() to play out the next sentence in the array
            if (continueKey && Input.GetButton("Text"))
                NextSentence();
        }
    }
    //Coroutine handling the typing
    public IEnumerator Type()
    {
        //For each letter in the sentence of index X (converted from string to char array)
        //Add a letter to the component 'text' of the TMP object TextDisplay, so it is displayed
        //and Wait for the time defined in "typingSpeed", before showing the next letter, etc.
        //To give the effect of a Typewriter machine, where all the text is written in real-time.
        foreach (char letter in sentences[index].ToCharArray())
        {

            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //When the method NextSentence() is called, play the "change" animation to make the next sentence fade in. 
    //and disable the text for the continueKey while the sentence is being written out. 
    public void NextSentence()
    {

        textDisplayAnim.SetBool("Change", true);
        continueKey.SetActive(false);
        //if the current index number is less than the total number of sentences in the string array (subtracted by 1, as the list starts at 0)
        //increase the index number by 1, to proceed to the next sentence on the list. 
        //Start the coroutine for the typing
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        //Otherwise, let the last sentence in the index stay displayed and set the continueKey to false, to indicate that the dialogue is finished
        else
        {
            textDisplay.text = "";
            continueKey.SetActive(false);
        }
    }

}
