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

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        controllerScript = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

         indexOfRoll = anim.GetLayerIndex("Base Layer");

        //Debug.Log("Base: " + anim.GetLayerIndex("Arma_Roll"));

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controllerScript.triggerGrounded;
        isGliding = controllerScript.gliding;
        isBalled = controllerScript.balled;

        Running = controllerScript.rb.velocity.x;


        if (Input.GetButton("Jump"))
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);
        }

        if (isGliding && !isGrounded)
        {
            anim.SetBool("isGliding", true);
            anim.SetBool("isGrounded", false);
        }

        if (isBalled && isGrounded && Input.GetButtonDown("Roll"))
        {
     
            anim.SetBool("isGrounded", true);
            anim.SetBool("isRolling", true);
        }

        if(isGrounded)
        {
            anim.SetBool("isGrounded", true);
            OnLandEvent.Invoke();
        }

        if(isGrounded && Running > 0f || Running < 0f)
        {
            anim.SetFloat("Running", Mathf.Abs(Running));
        }

    }

    public void Rolling()
    {
        anim.Play("Rolling", indexOfRoll);


           if (anim.GetBool("isRolling") == true && isGrounded && Input.GetButtonDown("Roll"))
            {
                anim.SetBool("isRolling", false);
            }
        
    }

    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);
    }
}
