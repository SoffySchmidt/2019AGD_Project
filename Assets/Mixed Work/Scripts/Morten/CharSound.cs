using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSound : MonoBehaviour
{

    AudioSource audioS;
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //audioS.pitch = 1 + transform.position.y / 200f;

        audioS.pitch = Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.magnitude / 12f, 0f, 1f);
            

        if (pc.triggerGrounded)
        {
            audioS.volume += Time.deltaTime * 2f;


        }
        else
        {
            audioS.volume -= Time.deltaTime / 1.5f;
        }

    }
}
