﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    public float lifeTime = 3.0f;
    public float moveSpeed = 50.0f;
    public float damage = 2.0f;
    public float staminaGain;
    public Vector2 MoveDirection;

    private PlayerController player;
    private Rigidbody2D rb;
    public GameObject gem;

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, lifeTime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    //travel forward
    private void Movement() {
        rb.velocity = MoveDirection * moveSpeed;
        //rb.AddForce(moveSpeed * MoveDirection);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var enemy = other.gameObject.GetComponent<EnemyBase>();
        if (enemy != null) {
            other.transform.GetComponent<EnemyBase>().TakeDamage(damage);
            player.CurStamina += staminaGain;
        }
        else if (other.tag == "Crate") {
            for (int j = 0; j < 7; j++) {
                int xGem = Random.Range(1, 4);
                int yGem = Random.Range(1, 4);
                Vector3 gemSpawn = new Vector3(xGem, yGem);
                Instantiate(gem, transform.position + gemSpawn, transform.rotation);
            }
            GameManager.instance.ComboTracker(3.0f);
            Destroy(other.gameObject);
            player.RandomWeapon();
        }
        else if (other.tag == "Switch")
            other.GetComponent<SwitchScript>().HitLever();
        else if (other.tag == "Shop") {
            if (Vector3.Distance(player.transform.position, other.transform.position) < 20) {
                var weapon = other.GetComponent<WeaponBuyScript>();
                var health = other.GetComponent<HealthUpgrade>();
                var stamina = other.GetComponent<StaminaUpgrade>();
                if (weapon != null && !weapon.Purchased) {
                    weapon.Purchased = player.UnlockWeapon(weapon.WeaponNumber, weapon.GemCount);
                }
                if (health != null) {
                    if (health.Purchasable && player.GiveHealth(health.Amount, health.GemCount))
                        health.Purchased();
                }
                if (stamina != null) {
                    if (stamina.Purchasable && player.GiveStamina(stamina.Amount, stamina.GemCount))
                        stamina.Purchased();
                }

            }
            else {
                //Have some error message show. Stops pistols from buying
            }
        }
        else if (other.tag == "Gems") {
            player.gemCount++;
            Destroy(other.gameObject);
        }
        else if (other.tag != "Trigger" && other.tag != "Player") {
            if (other.GetComponent<PlayerProjectile>() == null)
                Destroy(gameObject);
        }
    }
}
