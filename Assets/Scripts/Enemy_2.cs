using System.Collections;
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
            int number = Random.Range(1, 4);
            if (number == 1) 
                transform.position += Time.deltaTime * moveSpeed * transform.right;
            if (number == 2)
                transform.position += Time.deltaTime * moveSpeed * -transform.right;
            if (number == 3)
                transform.position += Time.deltaTime * moveSpeed * transform.up;
            if (number == 4)
                transform.position += Time.deltaTime * moveSpeed * -transform.up;
            newDirectionTime = Time.time + newDirectionRate;
        }

    }

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    
}
