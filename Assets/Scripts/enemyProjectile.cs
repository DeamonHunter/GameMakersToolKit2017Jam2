﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour {

    public float lifeTime = 200.0f;
    public float moveSpeed = 15.0f;
    public float enemyDamage = 2.0f;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    //travel forward
    private void Movement() {
        transform.position += Time.deltaTime * moveSpeed * transform.right;
    }

    //Have playertake damage when hit
    void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            other.transform.GetComponent<Player>().takeDamage(enemyDamage);
            Destroy(this.gameObject);
        }

    }
}
