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

    public bool notBalled;
    public float ballTimer;


    public float movement;
    public int indexOfRoll;






    [Header("Events")]
    [Space]

    //Handling method OnLanding() for resetting bool values to false 
    //Handling method Rolling() for activating Animation State for "Rolling", using an Animation Event 
    //at the last key frame over the "RollUp" animation.

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        controllerScript = FindObjectOfType<PlayerController>();
        rb = controllerScript.rb;
        anim = GetComponent<Animator>();

        //Resetting the bool values for jumping and gliding to false (see in the bottom) after pressing the button
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

         indexOfRoll = anim.GetLayerIndex("Base Layer");

        //Debug.Log("Base: " + anim.GetLayerIndex("Arma_Roll"));

    }

    // Update is called once per frame
    void Update()
    {
        //Accessing variables from PlayerController script 
        isGrounded = controllerScript.triggerGrounded;
        isGliding = controllerScript.gliding;
        isBalled = controllerScript.balled;
        isJumping = controllerScript.hasJumped;

        notBalled = controllerScript.unfold;
        ballTimer = controllerScript.ballTimer;

        slidingDown = controllerScript.movingDown;


        //Running = controllerScript.rb.velocity.x;
        movement = controllerScript.velocityRead;



        //MOVEMENT
        if (isGrounded && movement > 0f && Input.GetAxisRaw("Horizontal") != 0f)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isSliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);

            //
        } else if (isGrounded && movement > 0f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            anim.SetBool("isSliding", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);

        }

        else if (isGrounded && movement <= 0f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isSliding", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);
        }

        //JUMPING
        if (isJumping)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isSliding", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);

            if(isGrounded)
            {
                anim.SetBool("isLanding", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isSliding", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isGliding", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isRolling", false);
            }
        }

        //ROLLING
        if (movement > 8f && isBalled)
        {
            if (ballTimer > 0f && isGrounded)
            {
                anim.SetBool("isRolling", true);
            }
        }

        if (movement < 4f && !isBalled)
        {

            if (ballTimer < 0f && isGrounded)
            {
                anim.SetBool("isRolling", false);
            }
        }

        //LANDING


        //GROUNDED
        if (isGrounded)
        {
            /*
            anim.SetBool("isIdle", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isSliding", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            */
        }



    }
    //ROLLING: called by Animation Event in the "RollUp" animation
    public void Rolling()
    {
        anim.Play("Rolling", indexOfRoll); 
        
    }

    //GLIDING called by Animation Event in the "Jump" animation
    public void Gliding()
    {
        if(Input.GetButton("Jump") && !isGrounded)
        anim.Play("Gliding", indexOfRoll);
    }

    public void Sliding()
    {
        //anim.SetBool("isGrounded", true);
        //anim.SetBool("isSliding", true);

        if (slidingDown && isGrounded)
        {
            //anim.SetFloat("Running", 0f);
            anim.Play("Sliding", indexOfRoll);
            //anim.SetBool("isSliding", true);
        }


        
    }

}
