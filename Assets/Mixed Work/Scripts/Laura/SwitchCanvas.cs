using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class SwitchCanvas : MonoBehaviour
{
    public GameObject offCanvas, onCanvas, firstObject;

    public void Switch()
    {
        offCanvas.SetActive(true);
        onCanvas.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObject, null);
    }

}
