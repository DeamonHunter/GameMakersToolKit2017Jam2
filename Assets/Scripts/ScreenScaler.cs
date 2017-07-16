using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaler : MonoBehaviour {

    // Use this for initialization
    void Start() {
        transform.localScale = transform.localScale * Screen.height / 450;
    }

    // Update is called once per frame
    void Update() {

    }
}
