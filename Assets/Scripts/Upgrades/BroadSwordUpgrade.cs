using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadSwordUpgrade : MonoBehaviour {

    public PlayerController pc;

	// Use this for initialization
	void Start () {
        pc.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {
            pc.Weapons[0] = true;
        }
    }
}
