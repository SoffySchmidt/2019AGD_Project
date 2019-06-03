using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPos : MonoBehaviour
{
    public Camera cam;
    public GameObject shroom;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cam.WorldToScreenPoint(shroom.transform.position) + offset;
    }

}
