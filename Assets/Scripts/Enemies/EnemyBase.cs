using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    protected GameObject Player;
    public GameObject gem;
    public GameObject deathEffect;
    public GameObject FlyAwayText;

    public float moveSpeed = 5.0f;
    public float health = 1.0f;
    public float damage = 1.0f;
    public float damageRate = 0.2f;
    public bool Activated;
    private float damageTime;


    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        Activated = false;
    }

    // Update is called once per frame
    protected abstract void Update();

    public virtual void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            GameManager.instance.Combo++;
            for (int i = 0; i < GameManager.instance.Combo; i++) {
                for (int j = 0; j < 1; j++) {
                    float xGem = Random.Range(-4.0f, 4.0f);
                    float yGem = Random.Range(-4.0f, 4.0f);
                    Vector3 gemSpawn = new Vector3(xGem, yGem);
                    Instantiate(gem, transform.position + gemSpawn, transform.rotation);
                }
            }
            Instantiate(FlyAwayText, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.transform.tag == "Player" && Time.time > damageTime) {
            other.transform.GetComponent<PlayerController>().TakeDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }

    public void ActivateEnemy() {
        Activated = true;
    }
}
