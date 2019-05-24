﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernRough : MonoBehaviour
{

    public GameObject rolledIn;
    public GameObject rolledOut;
    public GameObject pathBlock;

    public bool rolled;

    public Animator Fern;
    
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
            pathBlock.SetActive(true);
            Fern.SetBool("isOut", true);
        }
    }

    public void Walkable()
    {
        pathBlock.SetActive(false);
        rolledOut.SetActive(true);
    }

}
