using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static float health = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage(float enemyDamage) {
        health -= enemyDamage;
        HealthManager.health -= enemyDamage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
