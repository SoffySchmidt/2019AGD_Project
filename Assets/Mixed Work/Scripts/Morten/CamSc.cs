using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSc : MonoBehaviour
{

    public Transform camPoint;
    Transform targetPoint;
    public GameObject player;

    public Transform[] rays;
    public int rayHits;

    public Vector3[] hitPoints;

    public GameObject debugTst;
    public Vector3 correctedPivot;
    public bool leftWall;
    public bool rightWall;
    public bool topWall;
    public bool buttomWall;

    public float speed;

    float zSpeed;
    Rigidbody2D rb2d;

    Vector3 targetZ;

    float impact;

    float impactTimer;

    public float zoomVel;

    public float zoomMax;
    public float zoomMin;


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


            targetZ = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, zoomMax), speed * zoomVel * Time.deltaTime);

        }
        else
        {
            speed = 0.018f;
            targetZ = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, zoomMin), speed * (10 + Mathf.Abs(targetZ.z * 1.5f)) * impact * Time.deltaTime);
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


        debugTst.transform.position = correctedPivot;


        correctedPivot = 
        (
        
        ((hitPoints[0] + player.transform.position) / 2f) * 3f
        + ((hitPoints[1] + player.transform.position) / 2f) * 3f
        + ((hitPoints[2] + player.transform.position) / 2f) * 3f
        + ((hitPoints[3] + player.transform.position) / 2f) * 3f
        + ((hitPoints[4] + player.transform.position) / 2f) * 1.2f
        + ((hitPoints[5] + player.transform.position) / 2f) * 1.2f
        + ((hitPoints[7] + player.transform.position) / 2f) * 1.2f
        + ((hitPoints[8] + player.transform.position) / 2f) * 0.6f
        + ((hitPoints[9] + player.transform.position) / 2f) * 0.6f
        + ((hitPoints[10] + player.transform.position) / 2f) * 0.6f
        + ((hitPoints[11] + player.transform.position) / 2f) * 0.6f
        + ((hitPoints[12] + player.transform.position) / 2f) * 0.6f
        + ((hitPoints[6] + player.transform.position) / 2f) * 0.6f
        ) / 19.2f;







        RaycastHit hit0;
        Debug.DrawRay(rays[0].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[0].position, Vector3.forward * 30f, out hit0, 30f))
        {
            if (hit0.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit0");
                hitPoints[0] = hit0.point;
            }
        }
        else
        {
            hitPoints[0] = player.transform.position;
        }

        RaycastHit hit1;
        Debug.DrawRay(rays[1].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[1].position, Vector3.forward * 30f, out hit1, 30f))
        {
            if (hit1.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit1");
                hitPoints[1] = hit1.point;
            }
        }
        else
        {
            hitPoints[1] = player.transform.position;
        }

        RaycastHit hit2;
        Debug.DrawRay(rays[2].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[2].position, Vector3.forward * 30f, out hit2, 30f))
        {
            if (hit2.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit2");
                hitPoints[2] = hit2.point;
            }
        }
        else
        {
            hitPoints[2] = player.transform.position;
        }

        RaycastHit hit3;
        Debug.DrawRay(rays[3].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[3].position, Vector3.forward * 30f, out hit3, 30f))
        {
            if (hit3.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit3");
                hitPoints[3] = hit3.point;
            }
        }
        else
        {
            hitPoints[3] = player.transform.position;
        }

        RaycastHit hit4;
        Debug.DrawRay(rays[4].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[4].position, Vector3.forward * 30f, out hit4, 30f))
        {
            if (hit4.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit4");
                hitPoints[4] = hit4.point;
            }
        }
        else
        {
            hitPoints[4] = player.transform.position;
        }

        RaycastHit hit5;
        Debug.DrawRay(rays[5].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[5].position, Vector3.forward * 30f, out hit5, 30f))
        {
            if (hit5.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit5");
                hitPoints[5] = hit5.point;
            }
        }
        else
        {
            hitPoints[5] = player.transform.position;
        }

        RaycastHit hit6;
        Debug.DrawRay(rays[6].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[6].position, Vector3.forward * 30f, out hit6, 30f))
        {
            if (hit6.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit6");
                hitPoints[6] = hit6.point;
            }
        }
        else
        {
            hitPoints[6] = player.transform.position;
        }

        RaycastHit hit7;
        Debug.DrawRay(rays[7].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[7].position, Vector3.forward * 30f, out hit7, 30f))
        {
            if (hit7.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit7");
                hitPoints[7] = hit7.point;
            }
        }
        else
        {
            hitPoints[7] = player.transform.position;
        }

        RaycastHit hit8;
        Debug.DrawRay(rays[8].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[8].position, Vector3.forward * 30f, out hit8, 30f))
        {
            if (hit8.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit8");
                hitPoints[8] = hit8.point;
            }
        }
        else
        {
            hitPoints[8] = player.transform.position;
        }

        RaycastHit hit9;
        Debug.DrawRay(rays[9].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[9].position, Vector3.forward * 30f, out hit9, 30f))
        {
            if (hit9.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit9");
                hitPoints[9] = hit9.point;
            }
        }
        else
        {
            hitPoints[9] = player.transform.position;
        }

        RaycastHit hit10;
        Debug.DrawRay(rays[10].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[10].position, Vector3.forward * 30f, out hit10, 30f))
        {
            if (hit10.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit10");
                hitPoints[10] = hit10.point;
            }
        }
        else
        {
            hitPoints[10] = player.transform.position;
        }

        RaycastHit hit11;
        Debug.DrawRay(rays[11].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[11].position, Vector3.forward * 30f, out hit11, 30f))
        {
            if (hit11.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit11");
                hitPoints[11] = hit11.point;
            }
        }
        else
        {
            hitPoints[11] = player.transform.position;
        }

        RaycastHit hit12;
        Debug.DrawRay(rays[12].position, Vector3.forward * 30f, Color.cyan);
        if (Physics.Raycast(rays[12].position, Vector3.forward * 30f, out hit12, 30f))
        {
            if (hit12.collider.CompareTag("outsideBorder"))
            {
                //Debug.Log("hit12");
                hitPoints[12] = hit12.point;
            }
        }
        else
        {
            hitPoints[12] = player.transform.position;
        }

    }
}
