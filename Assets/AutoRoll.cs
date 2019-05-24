using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRoll : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    public float power;
    public bool last;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {


            player.GetComponent<PlayerController>().balled = true;
            player.GetComponent<PlayerController>().enabled = false;

            rb.AddForce(transform.right * power * Time.deltaTime * 10f);

            if (last)
                player.GetComponent<PlayerController>().enabled = true;



        }
    }


}
