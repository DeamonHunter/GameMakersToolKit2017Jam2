using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    Text healthText;
    public PlayerController pc;

    // Use this for initialization
    void Start () {
        healthText = this.GetComponent<Text>();
        pc.GetComponent<PlayerController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        //update health
        healthText.text = "Health: " + pc.curHealth;
    }
}
