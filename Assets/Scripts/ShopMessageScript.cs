using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMessageScript : MonoBehaviour {
    public float Timeout;

    private float timeoutTime;
    private Text text;

    // Update is called once per frame
    void Update() {
        if (Time.time > timeoutTime)
            gameObject.SetActive(false);
    }

    public void Showtext(string message) {
        if (text == null)
            text = GetComponent<Text>();
        text.text = message;
        timeoutTime = Time.time + Timeout;
    }
}
