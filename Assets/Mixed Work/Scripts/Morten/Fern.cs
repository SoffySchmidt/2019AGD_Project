﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fern : MonoBehaviour
{

    public GameObject rolledIn;
    public GameObject rolledOut;

    public bool rolled;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rolled)
        {
            rolledIn.SetActive(true);
            rolledOut.SetActive(false);
        }
        else
        {
            rolledIn.SetActive(false);
            rolledOut.SetActive(true);
        }
    }

}
