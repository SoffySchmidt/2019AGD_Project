using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationSetup : MonoBehaviour
{
    public Animator anim;
    public PlayerController controllerScript;
    public bool isGrounded;
    public bool isGliding;
    public bool isBalled;
    public bool isJumping;
    public bool slidingDown;

    public bool notBalled;
    public float ballTimer;


    public float Running;
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
        Running = controllerScript.velocityRead;

        //RUNNING
        if (isGrounded && Running > 0f )
        {
            anim.SetFloat("Running", Mathf.Abs(Running));
            anim.SetBool("isGrounded", true);
            //anim.SetBool("isSliding", false);
        }

        //JUMPING
        if (isJumping)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);   
        }

        //ROLLING
        if (Running > 8f && isBalled)
        {
            if (ballTimer > 0f && isGrounded)
            {
                anim.SetBool("isRolling", true);
            }
        }

        if (Running < 4f && !isBalled)
        {

            if (ballTimer < 0f && isGrounded)
            {
                anim.SetBool("isRolling", false);
            }
        }

        //GROUNDED
        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
            OnLandEvent.Invoke();
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
    /*
    public void checkVelocity()
    {
        if(!slidingDown && isGrounded && anim.GetFloat("Running") < 0.01f)
        {
            anim.Play("Idle", indexOfRoll);
        }
        else if(isGrounded && anim.GetFloat("Running") > 0.1f)
        {
            anim.Play("Run", indexOfRoll);
        }
        
    }
    */

    //reset bool values to false
    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);
        anim.SetBool("isSliding", false);
    }
}
