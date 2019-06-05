using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;

    public Animator textDisplayAnim;
    public GameObject continueKey;


    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            textDisplayAnim.SetBool("Change", false);
            continueKey.SetActive(true);

         if(continueKey && Input.GetKeyDown(KeyCode.E))
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

            textDisplayAnim.SetBool("Change", true);
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
