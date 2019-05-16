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

        Running = controllerScript.rb.velocity.x;


        //RUNNING
        if (isGrounded && Running > 0f || Running < 0f)
        {
            anim.SetFloat("Running", Mathf.Abs(Running));
            anim.SetBool("isGrounded", true);
        }

        //JUMPING
        if (Input.GetButton("Jump") && !isGliding)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGliding", false);
        }

        //GLIDING
        if (isGliding && !isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                anim.SetBool("isGliding", true);
                anim.SetBool("isGrounded", false);
            }
        }

        else if(Input.GetButton("Jump") && isGliding & isGrounded)
        {
            anim.SetBool("isGliding", false);
            anim.SetBool("isGrounded", true);
        }

        //ROLLING
        if (isGrounded && isBalled && Input.GetButtonDown("Roll"))
        {
            anim.SetBool("isRolling", true);
        }

        //UNROLLING
        else if (Input.GetButtonDown("Roll") && anim.GetBool("isRolling") == true && isGrounded)
        {
            anim.SetBool("isRolling", false);
        }

        //GROUNDED
        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
            OnLandEvent.Invoke();
        }



    }
    //called by Animation Event in the "RollUp" animation
    public void Rolling()
    {
        anim.Play("Rolling", indexOfRoll);  
    }

    //reset bool values to false
    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);
    }
}
