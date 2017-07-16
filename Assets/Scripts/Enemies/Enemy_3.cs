using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : EnemyBase {

    public Vector2 MaxForce;
    private Vector2 currentSpeed;

    float force = 5f;

    private Rigidbody2D rb;

    // Update is called once per frame
    protected override void Update() {
        if (Activated)
            rb.velocity = currentSpeed;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (Activated)
            currentSpeed = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
    }

}
