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
    public int indexOfAnim;






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

         indexOfAnim = anim.GetLayerIndex("Base Layer");

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
            anim.SetBool("isUnrolling", false);

            //
        }

        if (isGrounded && movement > 0f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            anim.SetBool("isSliding", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);
            anim.SetBool("isUnrolling", false);

        }



        //JUMPING && GLIDING
        if (isJumping)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isSliding", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isGliding", false);
            anim.SetBool("isRolling", false);
            anim.SetBool("isLanding", false);
            anim.SetBool("isUnrolling", false);


        }

        //ROLLING
        if (movement > 8f && isBalled)
        {
            if (ballTimer > 0f && isGrounded)
            {
                anim.SetBool("isRolling", true);
                anim.SetBool("isSliding", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isGliding", false);
                anim.SetBool("isLanding", false);
                anim.SetBool("isUnrolling", false);
            }
        }

        if (movement < 4f && !isBalled)
        {

            if (ballTimer < 0f && isGrounded)
            {
                anim.SetBool("isUnrolling", true);
                anim.SetBool("isRolling", false);
                anim.SetBool("isSliding", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isGliding", false);
                anim.SetBool("isLanding", false);
            }

            anim.SetBool("isUnrolling", false);
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
        anim.Play("Rolling", indexOfAnim); 
        
    }

    //GLIDING called by Animation Event in the "Jump" animation
    public void Gliding()
    {
        if (Input.GetButton("Jump") && !isGrounded)
        {
            anim.Play("Gliding", indexOfAnim);
            anim.SetBool("isSliding", false);

            if (isGrounded)
            {
                anim.SetBool("isSliding", false);
                anim.SetBool("isLanding", true);
            }
        }
    }

}
