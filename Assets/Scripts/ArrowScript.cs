using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public GameObject Arrow;
    public bool ShowArrow;
    public float Blinkrate;

    private float lastShown;

    // Use this for initialization
    void Start() {
        Arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (ShowArrow && lastShown + Blinkrate < Time.time) {
            Arrow.SetActive(!Arrow.activeSelf);
            lastShown = Time.time;
        }
        else if (!ShowArrow) {
            Arrow.SetActive(false);
        }
    }

    public void SetDirection(int direction, bool levelDone) {
        if (ShowArrow)
            return;
        ShowArrow = true;
        Arrow.SetActive(true);
        int degrees;
        switch (direction) {
            case 0:
                degrees = 0;
                break;
            case 1:
                degrees = 180;
                break;
            case 2:
                degrees = -90;
                break;
            case 3:
                degrees = 90;
                break;
            default:
                degrees = 0;
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, levelDone ? degrees + 180 : degrees);
        lastShown = Time.time;
    }
}
