using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectedCamPos : MonoBehaviour
{

    public Transform player;
    public GameObject cam;
    CamSc camscrpt;

    // Start is called before the first frame update
    void Start()
    {
        camscrpt = cam.GetComponent<CamSc>();
    }

    // Update is called once per frame
    void Update()
    {

        float distY = player.transform.position.y - camscrpt.correctedPivot.y;

        float distX = player.transform.position.x - camscrpt.correctedPivot.x;

        //transform.position = new Vector3(player.position.x + distX * 2f, player.position.y + distY * 2f, 0f);

        transform.position = Vector3.Slerp(transform.position, new Vector3(player.position.x + distX * 4f, player.position.y + distY * 4f, 0f), 0.2f * Time.deltaTime);

    }
}
