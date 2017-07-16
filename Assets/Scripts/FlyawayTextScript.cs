using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyawayTextScript : MonoBehaviour {
    public float fadeTime;

    public TextMesh text;
    public float Speed;
    private float progress;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (progress < 1) {
            progress += Time.deltaTime;
            transform.position += Vector3.up * Speed * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, progress));
        }
        else {
            Destroy(gameObject);
        }
    }
}
