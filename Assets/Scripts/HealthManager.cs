﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public static float health;
    public Text text;

	// Use this for initialization
	void Start () {
        health = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Health: " + health;
		
	}
}
