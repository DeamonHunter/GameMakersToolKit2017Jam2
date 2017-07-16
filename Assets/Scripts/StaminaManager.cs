using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour {
    private PlayerController pc;
    public RectTransform StaminaBar;
    private RectTransform bar;

    // Use this for initialization
    void Start() {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bar = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        StaminaBar.sizeDelta = new Vector2(pc.CurStamina / pc.MaxStamina * 150, 30);
        bar.localScale = new Vector3(Mathf.Log(pc.MaxStamina / 100 + 1.6f), 0.7f, 1);
    }
}
