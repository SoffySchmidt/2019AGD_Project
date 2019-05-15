using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrip : MonoBehaviour
{
    public Transform gp;
    public Transform sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 1 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, gp.position.y, transform.position.z), 2f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(gp.position.x, transform.position.y, transform.position.z), 0.1f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, sprite.rotation, 20f * Time.deltaTime);
    }
}
