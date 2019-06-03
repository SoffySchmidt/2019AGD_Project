using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject Dialogue;
    public GameObject DialogueText;
    public Animator SpiritAnim;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SpiritAnim.SetBool("CircMove", true);
            DialogueText.SetActive(true);
            Dialogue.GetComponent<Dialogue>().enabled = true;   
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Dialogue.GetComponent<Dialogue>().enabled = false;
            DialogueText.SetActive(false);

            SpiritAnim.SetBool("CircMove", false);
            
        }
    }
}
