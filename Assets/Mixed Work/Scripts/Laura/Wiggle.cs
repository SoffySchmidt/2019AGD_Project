using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{

    public float amountDistX = 1f, amountDistY = 1f;
    public float speedDist = 1f;
    public float delayDist = 0.5f;
    public float amountRot = 1f;
    public float speedRot = 1f;
    public float delayRot = 0.5f;
    private float newRot;
    private float newPosX, newPosY;
    private bool shake;
    private bool isCam;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Rotate());
        StartCoroutine(Move());
        if (gameObject.name != "Main Camera")
        {
            shake = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            Shake();
            isCam = true;
        }
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            newRot = Random.Range(amountRot * -1, amountRot);
            yield return new WaitForSeconds(delayRot);
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            newPosX = Random.Range(amountDistX * -1, amountDistX);
            newPosY = Random.Range(amountDistY * -1, amountDistY);
            yield return new WaitForSeconds(delayDist);
        }
    }

    public void StartShake()
    {
        shake = true;
    }

    public void StopShake()
    {
        shake = false;
    }

    public void Shake()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + newPosX, speedDist * Time.deltaTime), Mathf.Lerp(transform.position.y, transform.position.y + newPosY, speedDist * Time.deltaTime), transform.position.z);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Mathf.Lerp(transform.rotation.z, transform.rotation.z + newRot, speedRot * Time.deltaTime), transform.rotation.w);
    }
}
