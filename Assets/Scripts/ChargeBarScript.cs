using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarScript : MonoBehaviour {
    public float Percantage;

    public GameObject middle;

    // Update is called once per frame
    void Update() {
        middle.transform.localScale = new Vector3(Percantage * 2, 0.6f, 1);
        middle.transform.localPosition = new Vector3(-10 + Percantage * 10, 0, -0.01f);
    }
}
