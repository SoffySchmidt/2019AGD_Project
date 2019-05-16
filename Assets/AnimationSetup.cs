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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controllerScript.triggerGrounded;
        isGliding = controllerScript.gliding;
        isBalled = controllerScript.balled;

        Running = controllerScript.velocityRead;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);
        }

        if (isGliding && !isGrounded)
        {
            anim.SetBool("isGliding", true);
            anim.SetBool("isGrounded", false);
        }

        if (isBalled && isGrounded)
        {
            anim.SetBool("isRolling", true);
            anim.SetBool("isGrounded", true);
        }

        if(isGrounded)
        {
            anim.SetBool("isGrounded", true);
            OnLandEvent.Invoke();
        }

        if(Running > 0f)
        {
            anim.SetFloat("Running", Mathf.Abs(Running));
        }

    }

    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);
    }
}
