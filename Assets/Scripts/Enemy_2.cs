﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float health = 2.0f;
    public GameObject enemyProjectile;
    public float enemyFireRate = 1f;
    private float enemyFireTime;
    private float newDirectionTime;
    private float newDirectionRate = 1.5f;
    private int movementDirection;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Shoot();
        Movement();
    }

    private void Shoot() {
        if (GameManager.instance.player && Time.time > enemyFireTime) {
            Vector3 dir = GameManager.instance.player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(enemyProjectile, transform.position, transform.rotation);
            enemyFireTime = Time.time + enemyFireRate;
        }
    }

    private void Movement() {
        if (Time.time > newDirectionTime) {
            movementDirection = Random.Range(1, 4);
            newDirectionTime = Time.time + newDirectionRate;
        }
        switch (movementDirection) {
            case 1:
                transform.position += Time.deltaTime * moveSpeed * transform.right;
                break;
            case 2:
                transform.position += Time.deltaTime * moveSpeed * -transform.right;
                break;
            case 3:
                transform.position += Time.deltaTime * moveSpeed * transform.up;
                break;
            case 4:
                transform.position += Time.deltaTime * moveSpeed * -transform.up;
                break;

        }
    }

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }


}
