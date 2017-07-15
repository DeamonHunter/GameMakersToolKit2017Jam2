using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour {

    Text gemText;
    public PlayerController pc;

	// Use this for initialization
	void Start () {
        gemText = this.GetComponent<Text>();
        pc.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        gemText.text = "Gems: " + pc.gemCount;
	}
}
