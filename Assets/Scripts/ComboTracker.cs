using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTracker : MonoBehaviour {

    public RectTransform ComboBar;
    private RectTransform bar;

	// Use this for initialization
	void Start () {
        bar = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //comboText.text = "Combo: " + gm.currentCombo;
        ComboBar.sizeDelta = new Vector2(GameManager.instance.currentCombo / GameManager.instance.maxCombo * 150, 30);
	}
}
