using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    public float xOffset = 4, speed = 1, speedSpeed = 10, alphaMin= 0.1f, alphaMax = 1, alphaSpeed = 0.01f;
    float newX, randX, newSpeed, newAlpha;
    bool moving, movedLeft;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!movedLeft)
        {
            if (!moving)
            {
                randX = Random.Range(0, xOffset);
                newX = transform.position.x - randX;
                moving = true;
            }
            else
            {
                newSpeed = Mathf.Lerp(0, speed, speedSpeed * Time.deltaTime);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, newX, newSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            if (transform.position.x <= newX + (randX / 5))
            {
                moving = false;
                movedLeft = true;
            }
        }
        else
        {
            if (!moving)
            {
                randX = Random.Range(0, xOffset);
                newX = transform.position.x + randX;
                moving = true;
            }
            else
            {
                newSpeed = Mathf.Lerp(0, speed, speedSpeed * Time.deltaTime);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, newX, newSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            if (transform.position.x >= newX - (randX / 5))
            {
                moving = false;
                movedLeft = false;
            }
        }
    }

    IEnumerator Flicker()
    {
        bool brighten = false;
        float alpha;
        while (true)
        {
            if (!brighten)
            {
                alpha = sr.color.a;
                for (float f = alpha; f > alpha - Random.Range(0.1f, 9.9f) && f > alphaMin; f = f - alphaSpeed)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, f);
                    yield return new WaitForSeconds(0.05f);
                }
                brighten = true;
            }
            else
            {
                alpha = sr.color.a;
                for (float f = alpha; f < alpha + Random.Range(0.1f, 9.9f) && f < alphaMax; f = f + alphaSpeed)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, f);
                    yield return new WaitForSeconds(0.05f);
                }
                brighten = false;
            }
            yield return null;
        }
    }
}
