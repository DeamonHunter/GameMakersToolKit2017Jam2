using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    protected GameObject Player;

    public float moveSpeed = 5.0f;
    public float health = 1.0f;
    public float damage = 1.0f;
    public float damageRate = 0.2f;
    private float damageTime;


    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected abstract void Update();

    public virtual void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.transform.tag == "Player" && Time.time > damageTime) {
            other.transform.GetComponent<PlayerController>().TakeDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }
}
