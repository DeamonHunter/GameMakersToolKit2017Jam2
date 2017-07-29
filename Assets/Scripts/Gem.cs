using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {
    public Vector2 MaxForce;

    float force = 5f;
    private bool moveTowardsPlayer;
    private Rigidbody2D rb;
    private float startedMovingTime;
    public float SpeedMult;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Vector2 rand = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
        rb.AddForce(rand * force, ForceMode2D.Impulse);
        Invoke("StopSimulation", 2);
    }

    private void StopSimulation() {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        moveTowardsPlayer = true;
        startedMovingTime = Time.time;
    }

    private void Update() {
        if (moveTowardsPlayer) {
            rb.velocity = (GameManager.instance.player.transform.position - transform.position).normalized * 3 * (1 + (Time.time - startedMovingTime) * SpeedMult);
        }
    }
}
