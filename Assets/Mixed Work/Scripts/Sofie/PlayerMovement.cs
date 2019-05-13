using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator; 

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool glide = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
       

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Glide"))
        {
            glide = true;
            animator.SetBool("IsGliding", true);
        }
        else if (Input.GetButtonUp("Glide"))
        {
            glide = false;
            animator.SetBool("IsGliding", false);
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping",false);
        animator.SetBool("IsGliding", false);
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, glide, jump);
        jump = false;
        glide = false;

    }
}
