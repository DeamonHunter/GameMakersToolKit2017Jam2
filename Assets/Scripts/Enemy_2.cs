﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public float health = 1.0f;
    public float damage = 1.0f;
    private float damageRate = 0.2f;
    private float damageTime;
    public Transform target;
    public GameObject enemyProjectile;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    private void Movement() {
        if (target) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed *
            Time.deltaTime);
            Instantiate(enemyProjectile, transform.position, transform.rotation);
        }


    }

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.transform.tag == "Player" && Time.time > damageTime) {
            other.transform.GetComponent<Player>().takeDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }
}
