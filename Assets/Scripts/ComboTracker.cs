using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTracker : MonoBehaviour {

    public GameManager gm;
    Text comboText;

	// Use this for initialization
	void Start () {
        comboText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        comboText.text = "Combo: " + gm.currentCombo;
	}
}
