using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSetup : MonoBehaviour
{
    public Animator anim;
    public PlayerController controllerScript;
    Rigidbody2D rb;
    public bool isGrounded;
    public bool isGliding;
    public bool isBalled;
    public bool isJumping;
    public bool slidingDown;
    public bool isLanding;
    public bool isFalling;
    bool isRunning;


    public bool isIdle;
    public bool isRolling;
    public bool isSliding;
    public bool isUnrolling;
    public bool isRollingIn;

    public int animState;

    public bool notBalled;
    public float ballTimer;


    public float movement;
    public int indexOfAnim;


    //Handling method OnLanding() for resetting bool values to false 
    //Handling method Rolling() for activating Animation State for "Rolling", using an Animation Event 
    //at the last key frame over the "RollUp" animation.

    private void Awake()
    {
        controllerScript = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Resetting the bool values for jumping and gliding to false (see in the bottom) after pressing the button

         indexOfAnim = anim.GetLayerIndex("Base Layer");

        //Debug.Log("Base: " + anim.GetLayerIndex("Arma_Roll"));
    }


    void AnimSwitch(int state)
    {

        animState = state;

    }

    // Update is called once per frame
    void Update()
    {

        //Accessing variables from PlayerController script 

        //isGrounded = controllerScript.triggerGrounded;
        //isGliding = controllerScript.gliding;
        //isBalled = controllerScript.balled;
        //isJumping = controllerScript.hasJumped;

        //notBalled = controllerScript.unfold;
        //ballTimer = controllerScript.ballTimer;

        //slidingDown = controllerScript.movingDown;


        //Running = controllerScript.rb.velocity.x;

        //movement = controllerScript.velocityRead;

        if (controllerScript.balled)
        {
            AnimSwitch(10);
        }
        else if (controllerScript.gliding)
        {
            AnimSwitch(3);
        }
        else if (!controllerScript.grounded && rb.velocity.y <= 0f)
        {
            AnimSwitch(9);
        }
        else if (!controllerScript.grounded && rb.velocity.y > 0f)
        {
            AnimSwitch(1);
        }
        else if (controllerScript.grounded && controllerScript.hasJumped)
        {
            AnimSwitch(7);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0f && controllerScript.triggerGrounded)
        {
            AnimSwitch(6);
        }
        else if (controllerScript.rb.velocity.magnitude > 1.2f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            AnimSwitch(5);
        }
        else if (controllerScript.rb.velocity.magnitude <= 1.2f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            AnimSwitch(2);
        } 

        // ANIMATION CALLER

        //isJumping
        if (animState == 1)
        {
            anim.SetBool("isJumping",       true);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isIdle
        if (animState == 2)
        {
            anim.SetBool("isJumping",   false);
            anim.SetBool("isIdle",          true);
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isGliding
        if (animState == 3)
        {
            anim.SetBool("isJumping",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isGliding",       true);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isRolling
        if (animState == 4)
        {
            anim.SetBool("isRolling",       true);
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isSliding
        if (animState == 5)
        {
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isSliding",       true);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isRunning
        if (animState == 6)
        {
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",       true);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isLanding
        if (animState == 7)
        {
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",       true);
            anim.SetBool("isRollingIn", false);
        }
        //isUnrolling
        if (animState == 8)
        {
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling",     true);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isFalling
        if (animState == 9)
        {
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",       true);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn", false);
        }
        //isRollingIn
        if (animState == 10)
        {
            anim.SetBool("isSliding",   false);
            anim.SetBool("isRunning",   false);
            anim.SetBool("isIdle",      false);
            anim.SetBool("isJumping",   false);
            anim.SetBool("isGliding",   false);
            anim.SetBool("isRolling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isUnrolling", false);
            anim.SetBool("isFalling",   false);
            anim.SetBool("isLanding",   false);
            anim.SetBool("isRollingIn",     true);
        }

        //LANDING


    }
    //ROLLING: called by Animation Event in the "RollUp" animation


}
