using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    int force = 100;

	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().AddForce(transform.position * force, ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
