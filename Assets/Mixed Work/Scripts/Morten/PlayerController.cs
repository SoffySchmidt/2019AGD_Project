using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ballTest;

    public float velocityRead;
    public float hnormalRead;
    public float impactRead;
    public PhysicsMaterial2D matSlip;
    public PhysicsMaterial2D matNonSlip;
    public Rigidbody2D rb;
    public float oriSpeed;
    public float speedMul;
    float step;
    Vector3 dir;
    Vector3 spriteDir;
    float spriteDirMul;
    public Transform groundPoint;
    public GameObject sprite;

    public float glideTimer;
    float jumpTimer;
    public float jumpForce;
    public float glideDiv;
    public float takeoffForce;
    Vector3 targetDir;
    float turnSpeed;
    public float downForce;
    public float momMulti;
    float glidey;
    public bool grounded;
    public bool triggerGrounded;
    public bool balled;
    public bool gliding;
    bool hasJumped;
    bool isHit;
    public float intoBallTimer;
    public float outofBallTimer;
    public float ballTimer;
    bool unfold;

    Vector2 prevVelocity, deltaVelocity;

    Vector2 direction;


    Vector2 hNormal;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        GetComponent<Collider2D>().sharedMaterial = matNonSlip;
        oriSpeed = speedMul;
        ballTimer = intoBallTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // READS ONLY

        velocityRead = rb.velocity.magnitude;
        hnormalRead = hNormal.y;
        impactRead = deltaVelocity.magnitude;

        ballTest = Mathf.Clamp(Mathf.Clamp(1 - rb.velocity.magnitude, 0.4f, 0.5f) * (10.2f - Mathf.Abs(Input.GetAxisRaw("Horizontal")) * 10f) * (hNormal.y * 5 - 4f), 0.5f, 100f);

        // CONSTANTS

        if (jumpTimer > 0f && triggerGrounded)
        {
            speedMul = oriSpeed / 3f;
        }
        else if (grounded)
        {
            speedMul = oriSpeed;
        }
        else
        {
            speedMul = Mathf.Clamp(rb.velocity.magnitude * 40f, 0, oriSpeed);             
        }

        step = speedMul * (-Mathf.Abs(rb.velocity.x * .15f) + 2f) * Time.deltaTime;

        if (!grounded && rb.gravityScale < 3.7f)
            rb.gravityScale += Time.deltaTime * glidey;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f);

        if(hit.collider != null)
            Debug.DrawRay(hit.point, hit.normal * 2, Color.green);

        Debug.DrawRay(transform.position, Vector2.down * 10f, Color.red);

        if (hit.collider != null)
        {
            hNormal = hit.normal;
        }

        direction = Vector2.up;

        deltaVelocity = rb.velocity - prevVelocity;
        prevVelocity = rb.velocity;

        // SPRITES

        if (grounded)
        {
            sr.color = Color.green;

            if (hit.collider != null)
            {
                direction = hit.normal;
            }
            targetDir = direction;
            turnSpeed = 0.05f;
        }
        else
        {
            sr.color = Color.red;

            targetDir = (direction + Vector2.up * (Vector2.Distance(hit.point, transform.position) / 100f));
            //Vector3.Normalize(targetDir);


        }


        sprite.transform.up = Vector3.Slerp(sprite.transform.up, targetDir, turnSpeed);


        // BALL

        if (rb.velocity.magnitude > 8f && !balled)
        {

            if (ballTimer < 0f && triggerGrounded)
            {
                balled = true;
                unfold = false;
                ballTimer = outofBallTimer;
            }
            else
            {
                ballTimer -= Time.deltaTime;
            }

            //if(grounded)
                //rb.AddForce(sprite.transform.up * 200f);

        } else if (rb.velocity.magnitude < 4f)
        {

            if (ballTimer < 0f)
            {
                balled = false;
                ballTimer = intoBallTimer;
            }
            else
            {
                ballTimer -= Time.deltaTime * Mathf.Clamp(Mathf.Clamp(1 - rb.velocity.magnitude, 0.4f, 0.5f) * (10.2f - Mathf.Abs(Input.GetAxisRaw("Horizontal")) * 10f) * (hNormal.y * 5 - 4f), 0.5f, 100f);
            }

            if (deltaVelocity.magnitude > 7f)
                balled = false;
        }

        if (!balled && !unfold)
        {
            rb.AddForce(-rb.velocity * 10f);
            rb.AddForce(sprite.transform.up * 150f + Vector3.up * 450f);
            unfold = true;
        }


        // HORIZONTAL MOVEMENT
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            dir = transform.right;
            spriteDir = sprite.transform.right;

            sprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            dir = -transform.right;
            spriteDir = -sprite.transform.right;

            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }



        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") > 0 && rb.velocity.y > 0.5f)
        {
            if (!balled)
            {
                spriteDirMul = (1 - hNormal.y) * 6.5f;
            }
            else
            {
                spriteDirMul = 1f;
            }

            if (balled)
                momMulti = Mathf.Clamp(momMulti + (rb.velocity.magnitude * 2f) * Time.deltaTime * 0.05f, -7f, 7f);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(dir * step + (spriteDir * spriteDirMul) * step * momMulti * Mathf.Clamp(hNormal.y * 1.5f, 0f, 1f));
            }
            else
            {
                rb.AddForce(dir * step + spriteDir * step * momMulti);
            }

            

        }
        else
        {
            rb.AddForce(-rb.velocity);

            momMulti = 0.5f;
        }

        // JUMP



        if (Input.GetButtonDown("Jump") && triggerGrounded && !hasJumped && !balled && jumpTimer < 0f)
        {
            rb.AddForce(-rb.velocity * 15f + Vector2.up * jumpForce * (0.3f + hNormal.y));
            glideTimer = 2f;
            hasJumped = true;
            jumpTimer = 1f;
        }
        else
        {
            jumpTimer -= Time.deltaTime;
        }

        if (Input.GetButton("Jump") && !triggerGrounded || isHit)
        {
            glideTimer -= Time.deltaTime;
            rb.AddForce(transform.up * glideDiv * glideTimer);

            gliding = true;

            glidey = 0;
        }
        else
        {
            glidey = 1.8f;

            gliding = false;
        }



        // DOWNWARD FORCE

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //rb.AddForce(-sprite.transform.up * Mathf.Abs(rb.velocity.x) * downForce * Time.deltaTime + -transform.up * downForce * Time.deltaTime);

            float sweetSport = Mathf.Clamp(Vector2.Distance(hit.point, transform.position), 0, 1f);
            rb.AddForce(-sprite.transform.up * Mathf.Abs(rb.velocity.x) * downForce * (sweetSport * 2f) * Time.deltaTime);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        hasJumped = false;

        if (collision.gameObject.CompareTag("fernRolled"))
        {
            Debug.Log("going out");
            collision.gameObject.GetComponentInParent<Fern>().rolled = false;
            rb.AddForce(-rb.velocity);
            rb.AddForce(transform.up * 200f);
            isHit = true;
        }

        if (collision.gameObject.CompareTag("fernOut"))
        {
            Debug.Log("going in");
            collision.gameObject.GetComponentInParent<Fern>().rolled = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isHit = false;
            //check normal y transform
            //check y velocity

            //            if (rb.velocity.y <= -0.5f || hNormal.y > 0.6f)


            bool tooSteep = hNormal.y < 0f;
            bool movingDown = rb.velocity.y < 0f;
            float slideTDown = 0.5f;
            float slideTUp = 0.3f;



            if (hNormal.y < slideTDown)
            {
                hNormal.y = -1;
                movingDown = true;
            }
            else if (!movingDown && hNormal.y < slideTUp)
            {
                hNormal.y = -1;
            }



            if (!grounded || balled)
            {
                GetComponent<Collider2D>().sharedMaterial = matSlip;
            }
            else
            {
                GetComponent<Collider2D>().sharedMaterial = matNonSlip;
            }



            grounded = true;

            if(movingDown && hNormal.y < 0.5f || tooSteep || !triggerGrounded)
            {
                grounded = false;
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

            //rb.mass = 0.5f;

            if (balled)
            {
                rb.gravityScale = 1f;
            }
            else
            {
                rb.gravityScale = 2.2f;
            }


            triggerGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            //rb.mass = 0.35f;

            grounded = false;

            triggerGrounded = false;

            rb.gravityScale = 1f;

            groundPoint.position = transform.position;

            takeoffForce = Mathf.Abs(rb.velocity.x);

            groundPoint.position = transform.position;

        }
    }

}
