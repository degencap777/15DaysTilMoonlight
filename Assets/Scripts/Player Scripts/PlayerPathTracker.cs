﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathTracker : MonoBehaviour
{
    public string pathName;
    //private BoxCollider2D collider;
    // Use this for initialization
    void Start()
    {
        //collider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PathMaster")
        {
            pathName = other.gameObject.name;
        }
    }
}
