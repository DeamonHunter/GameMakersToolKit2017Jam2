using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTracker : MonoBehaviour {

    public RectTransform ComboBar;
    private RectTransform bar;
    private Text text;
    private int lastCombo;

    // Use this for initialization
    void Start() {
        bar = GetComponent<RectTransform>();
        text = GetComponentInChildren<Text>();
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.Combo >= 1) {
            if (lastCombo < 1) {
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);
            }
            ComboBar.sizeDelta = new Vector2(GameManager.instance.comboTimeRemaining / GameManager.instance.maxComboTime * 150, 30);
            text.text = GameManager.instance.Combo.ToString();
            lastCombo = GameManager.instance.Combo;
        }
        else if (lastCombo > 1) {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
            lastCombo = GameManager.instance.Combo;
        }
    }
}
