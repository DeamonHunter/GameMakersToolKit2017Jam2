using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
    public float HurtCooldown;
    public float Damage;
    private float lastHurtTime;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            if (lastHurtTime < Time.time) {
                lastHurtTime = Time.time + HurtCooldown;
                other.GetComponent<PlayerController>().TakeDamage(Damage);
            }
        }
    }
}
