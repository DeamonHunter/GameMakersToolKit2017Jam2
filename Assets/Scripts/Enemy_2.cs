using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float health = 2.0f;
    public GameObject enemyProjectile;
    public float enemyFireRate = 0.40f;
    private float enemyFireTime;
    public Transform target;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Shoot();
    }

    private void Shoot() {
        if (target && Time.time > enemyFireTime) {
            Instantiate(enemyProjectile, transform.position, transform.rotation);
            enemyFireTime = Time.time + enemyFireRate;
        }
    }


    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    
}
