using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {
    public Vector2 MaxForce;

    float force = 5f;

    // Use this for initialization
    void Start() {
        Vector2 rand = new Vector2(Random.Range(-MaxForce.x, MaxForce.x), Random.Range(-MaxForce.y, MaxForce.y));
        this.GetComponent<Rigidbody2D>().AddForce(rand * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() {

    }
}
