using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour {

    public float lifeTime = 3.0f;
    public float moveSpeed = 50.0f;
    public float damage = 2.0f;

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    //travel forward
    private void Movement() {
        transform.position += Time.deltaTime * moveSpeed * transform.right;
    }

    //have player take damage when hit
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player") {
            Destroy(this.gameObject);
            other.transform.GetComponent<PlayerController>().TakeDamage(damage);

        }
    }
}
