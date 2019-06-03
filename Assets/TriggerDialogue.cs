using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject Dialogue;
    public GameObject DialogueText;
    public GameObject ContinueKey;
    public Animator SpiritAnim;
    int indexNum;
    // Update is called once per frame

    private void Update()
    {
        if (Dialogue.GetComponent<Dialogue>().index == 0)
        {
            SpiritAnim.Play("WaterSpirit_Idle");
        }

        if (Dialogue.GetComponent<Dialogue>().index == 1)
        {
            SpiritAnim.Play("WaterSpirit_Panic");
        }

        if (Dialogue.GetComponent<Dialogue>().index == 2)
        {
            SpiritAnim.Play("WaterSpirit_Fly");
        }

        if (Dialogue.GetComponent<Dialogue>().index == 3)
        {
            SpiritAnim.Play("WaterSpirit_FlyIdle");
        }
        if (Dialogue.GetComponent<Dialogue>().index == 4)
        {
            SpiritAnim.Play("WaterSpirit_FlyOff");
            DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
            Dialogue.GetComponent<Dialogue>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            DialogueText.SetActive(true);
            ContinueKey.SetActive(true);
            Dialogue.GetComponent<Dialogue>().enabled = true;

        }
        
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Dialogue.GetComponent<Dialogue>().enabled = false;
            DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
        }
    }
    

}
