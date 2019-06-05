using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject DialogueManager;
    public GameObject DialogueText;
    public GameObject ContinueKey;
    public Animator SpiritAnim;
    int indexNum;
    // Update is called once per frame

    private void Update()
    {
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
        if (DialogueManager.GetComponent<Dialogue>().index == 4)
        {
            SpiritAnim.Play("WaterSpirit_FlyOff");
        }
        if (DialogueManager.GetComponent<Dialogue>().index == 6)
        {
            SpiritAnim.Play("WaterSpirit_FlyOff2");
            //DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
            DialogueManager.GetComponent<Dialogue>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().radius = 1;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 15;
            DialogueManager.GetComponent<Dialogue>().enabled = true;
            DialogueText.SetActive(true);
            ContinueKey.SetActive(true);
            

        }
        
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 9;
            DialogueManager.GetComponent<Dialogue>().enabled = false;
            DialogueText.SetActive(false);
            ContinueKey.SetActive(false);
        }
    }
    

}
