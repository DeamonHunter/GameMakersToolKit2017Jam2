using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CameraController : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        var pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
