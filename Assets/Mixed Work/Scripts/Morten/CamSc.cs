using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSc : MonoBehaviour
{

    public Transform camPoint;
    Transform targetPoint;
    public GameObject player;

    public float speed;

    float zSpeed;
    Rigidbody2D rb2d;

    Vector3 targetZ;

    float impact;

    float impactTimer;

    public float zoomVel;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform;
        rb2d = player.GetComponent<Rigidbody2D>();
        targetZ = transform.position;
        impact = 1;
    }

    Vector2 prevVelocity, deltaVelocity;
    // Update is called once per frame
    void FixedUpdate()
    {

        if (deltaVelocity.magnitude > 3f)
        {
            impact = deltaVelocity.magnitude / 6f;
            impactTimer = 0.7f;
        }
        else if (impact > 1f)
        {
            impact -= 2 * Time.deltaTime;
        }

        impactTimer -= Time.deltaTime;

        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 2f)
        {
            //zSpeed = (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) + Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y / 6f)) * 3f;
        }

        if (Mathf.Abs(rb2d.velocity.x) > 1.5f && impactTimer < 0f)
        {

            if (rb2d.velocity.magnitude > 8f)
            {
                zoomVel = rb2d.velocity.magnitude;
                speed += 0.003f * Time.deltaTime;
            }
            else
            {
                zoomVel = 0;
                speed = 0f;
            }


            targetZ = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, -70), speed * zoomVel * Time.deltaTime);

        }
        else
        {
            speed = 0.018f;
            targetZ = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, -40), speed * (10 + Mathf.Abs(targetZ.z * 1.5f)) * impact * Time.deltaTime);
        }




        float distX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(camPoint.position.x, 0));
        float distY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, camPoint.position.y));

        targetPoint.position = Vector3.MoveTowards(targetPoint.position, camPoint.position, (distY * 15f + 17f) * Time.deltaTime);



        transform.position = new Vector3(camPoint.transform.position.x, targetPoint.position.y, targetZ.z);

    }

    private void Update()
    {
        deltaVelocity = rb2d.velocity - prevVelocity;
        prevVelocity = rb2d.velocity;
    }
}
