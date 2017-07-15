using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    private PlayerController pc;
    public RectTransform HealthBar;
    private RectTransform bar;

    // Use this for initialization
    void Start() {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bar = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        HealthBar.sizeDelta = new Vector2(pc.curHealth / pc.MaxHealth * 200, 30);
        bar.localScale = new Vector3(Mathf.Log(pc.MaxHealth / 10 + 1), 1, 1);
    }
}
