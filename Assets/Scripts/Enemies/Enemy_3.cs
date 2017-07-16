using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : EnemyBase {

    public Vector2 MaxForce;

    float force = 5f;

    // Update is called once per frame
    protected override void Update() {

    }

    void Start() {
        if (Activated) {
            Vector2 rand = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
            this.GetComponent<Rigidbody2D>().AddForce(rand * force, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (Activated) { 
        Vector2 rand = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
        this.GetComponent<Rigidbody2D>().AddForce(rand * force, ForceMode2D.Impulse);
        }
    }

}
