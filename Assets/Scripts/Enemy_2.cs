using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float health = 2.0f;
    public GameObject enemyProjectile;
    public float enemyFireRate = 1f;
    private float enemyFireTime;


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

    }


    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    
}
