using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElderSpiritsDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    //public int lineLength;
    //string line;
    //string alphabet;
    //char[] charToValidate;

    //public Animator textDisplayAnim;
    public GameObject continueKey;


    void Start()
    {
        //lineLength = 40;
        //alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
        //charToValidate = alphabet.ToCharArray();
        //line = "";
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueKey.SetActive(true);

            if (continueKey && Input.GetButton("Text"))
                NextSentence();
        }
    }

    public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {

        //textDisplayAnim.SetTrigger("Change");
        continueKey.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        else
        {
            textDisplay.text = "";
            continueKey.SetActive(false);
        }
    }

}
