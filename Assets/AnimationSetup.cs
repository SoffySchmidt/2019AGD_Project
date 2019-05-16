using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetup : MonoBehaviour
{
    Animator anim;
    public bool isGrounded;
    public bool isGliding;
    public bool isBalled;
    public float Running;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = gameObject.GetComponent<PlayerController>().triggerGrounded;
        isGliding = gameObject.GetComponent<PlayerController>().gliding;
        isBalled = gameObject.GetComponent<PlayerController>().balled;

        Running = gameObject.GetComponent<PlayerController>().velocityRead;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("isJumping", true);
        }

        if (isGliding)
        {
            anim.SetBool("isGliding", true);
        }

        if (isBalled)
        {
            anim.SetBool("isRolling", true);
        }

        if(isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }

        if(Running > 0f)
        {
            anim.SetFloat("Running", Mathf.Abs(Running));
        }

        if(isBalled && Input.GetButton("Jump") && isGrounded)
        {
            anim.SetTrigger("trigRolling");
        }
    }

    void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);
    }
}
