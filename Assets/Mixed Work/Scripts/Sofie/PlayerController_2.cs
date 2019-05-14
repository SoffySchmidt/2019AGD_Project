using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController_2 : MonoBehaviour
{


    protected Rigidbody2D rb;
    public float speedMul;
    float step;
    protected Vector3 dir;
    Vector3 spriteDir;
    public Transform groundPoint;
    public GameObject sprite;
    public bool grounded;
    bool triggerGrounded;
    public float jumpTimer;
    public float jumpForce;
    public float takeoffForce;
    Vector3 targetDir;
    float turnSpeed;
    public float downForce;
    public float momMulti;
    float glidey;
    public Animator anim;
    public float horizontalMove;

    public GameObject test;

    Vector2 direction;

    public float test2;

    Vector2 hNormal;

    SpriteRenderer sr;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        test2 = rb.velocity.y;

        test.transform.position = transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        // CONSTANTS
        if (grounded)
        {
            //ANIMATOR "ISGROUNDED" PARAMETER 
            anim.SetBool("isGrounded", true);
            step = speedMul * (-Mathf.Abs(rb.velocity.x * .15f) + 2f) * Time.deltaTime;
        }
        else
        {
            step = speedMul / 1.1f * (-Mathf.Abs(rb.velocity.x * .15f) + 2f) * Time.deltaTime;
        }

        if (!grounded && rb.gravityScale < 3.7f)
            rb.gravityScale += Time.deltaTime * glidey;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f);

        if (hit.collider != null)
            Debug.DrawRay(hit.point, hit.normal * 2, Color.green);

        Debug.DrawRay(transform.position, Vector2.down * 10f, Color.red);

        if (hit.collider != null)
        {
            hNormal = hit.normal;
        }

        direction = Vector2.up;

        // SPRITES

        if (grounded)
        {
            sr.color = Color.green;

            if (hit.collider != null)
            {
                direction = hit.normal;
            }
            targetDir = direction;
            turnSpeed = 0.2f;
        }
        else
        {
            sr.color = Color.red;

            targetDir = (direction + Vector2.up * (Vector2.Distance(hit.point, transform.position) / 10f));
            //Vector3.Normalize(targetDir);


        }


        sprite.transform.up = Vector3.Slerp(sprite.transform.up, targetDir, turnSpeed);


        // HORIZONTAL MOVEMENT

        // ANIMATOR "RUNNING" PARAMETER--------------------
        horizontalMove = Input.GetAxisRaw("Horizontal") * rb.velocity.magnitude;

        anim.SetFloat("Running", Mathf.Abs(horizontalMove));

        // ANIMATOR "RUNNING" PARAMETER----------------------

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            dir = transform.right;
            spriteDir = sprite.transform.right;

            sprite.transform.localScale = new Vector3(1, 1, 1);

            if(Input.GetButtonDown("Roll") && horizontalMove > 5f)
            {
                anim.SetTrigger("trigRolling");
                Rolling();
            }

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            dir = -transform.right;
            spriteDir = -sprite.transform.right;

            sprite.transform.localScale = new Vector3(-1, 1, 1);

            if (Input.GetButtonDown("Roll") && horizontalMove < -5f)
            {
                anim.SetTrigger("trigRolling");
                Rolling();
            }
        }


        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.AddForce(-rb.velocity);

            momMulti = 1f;

            if (Input.GetButtonDown("Roll") && horizontalMove == 0)
            {
                anim.SetTrigger("trigRolling");
                Rolling();
            }
        }
        else
        {
            rb.AddForce(dir * step + spriteDir * step * momMulti);

           if (rb.velocity.magnitude > 10f)
                momMulti = Mathf.Clamp(momMulti + (rb.velocity.magnitude * 2f) * Time.deltaTime * 0.05f, -4f, 4f);


        }

        // JUMP

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
            jumpTimer = 1.3f;

            //ANIMATOR "ISJUMPING PARAMETER------------
            anim.SetBool("isJumping", true);
        }

        if (Input.GetButton("Jump"))
        {
            jumpTimer -= Time.deltaTime;
            rb.AddForce(transform.up * (jumpForce / 150) * jumpTimer);

            glidey = 0;

        }

        else
        {
            glidey = 1f;
            anim.SetBool("isJumping", false);

        }

        //GLIDE


        if (Input.GetButtonDown("Glide") && !grounded)
        {
            rb.AddForce(transform.up * jumpForce);
            jumpTimer = 1.3f;

            anim.SetBool("isGliding", true);
        }

        if (Input.GetButton("Glide"))
        {
            jumpTimer -= Time.deltaTime;
            rb.AddForce(transform.up * (jumpForce / 150) * jumpTimer);

            glidey = 0;

        }

        else
        {
            glidey = 1f;
            anim.SetBool("isGliding", false);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //rb.AddForce(-sprite.transform.up * Mathf.Abs(rb.velocity.x) * downForce * Time.deltaTime + -transform.up * downForce * Time.deltaTime);

            float sweetSport = Mathf.Clamp(Vector2.Distance(hit.point, transform.position), 0, 1f);
            rb.AddForce(-sprite.transform.up * Mathf.Abs(rb.velocity.x) * downForce * (sweetSport * 2f) * Time.deltaTime);
        }


    }

    //METHOD FOR "ISROLLING" PARAMETER
    private void Rolling()
    {
        if (anim.GetBool("isRolling") == false)
            anim.SetBool("isRolling", true);

        else if (anim.GetBool("isRolling") == true)
            anim.SetBool("isRolling", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {

            //check normal y transform
            //check y velocity

            //            if (rb.velocity.y <= -0.5f || hNormal.y > 0.6f)

            if (hNormal.y < 0.3f)
                hNormal.y = -1;

            bool tooSteep = hNormal.y < 0;
            bool movingDown = rb.velocity.y < 0;

            if (movingDown)
            {
                grounded = true;
                
            }
            else if (!movingDown && !tooSteep)
            {
                grounded = true;
                
            }
            else if (!triggerGrounded)
            {
                grounded = false;
                //ANIMATOR "ISGROUNDED" PARAMETER
                anim.SetBool("isGrounded", false);
            }
;


            ContactPoint2D point = collision.GetContact(0);


            groundPoint.position = point.point;

            var dir = transform.position - groundPoint.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            groundPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("ground"))
        {
            anim.SetBool("isGrounded", true);
            //rb.mass = 0.5f;
            rb.gravityScale = 1.7f;
            OnLandEvent.Invoke();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            //rb.mass = 0.35f;

            triggerGrounded = false;

            rb.gravityScale = 1f;

            groundPoint.position = transform.position;

            takeoffForce = Mathf.Abs(rb.velocity.x);

            groundPoint.position = transform.position;
        }
    }
    //ANIMATOR PARAMETERS - ONLANDING RESET TO FALSE
    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isGliding", false);

    }

}
