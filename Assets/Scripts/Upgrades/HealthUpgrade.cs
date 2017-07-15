using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour {

    public PlayerController pc;
    public GameObject upgradeEffect;

    // Use this for initialization
    void Start() {
        //pc.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player") {
            pc.MaxHealth += 1;
            Instantiate(upgradeEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
