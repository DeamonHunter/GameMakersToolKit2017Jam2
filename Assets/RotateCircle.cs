using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour {

    float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, moveSpeed *
            Time.deltaTime);
    }

}
