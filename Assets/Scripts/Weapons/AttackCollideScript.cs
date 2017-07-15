using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollideScript : MonoBehaviour {
    public PlayerController Player;
    public float StaminaGainFromEnemy;

    //private float damageTime;
    //private float damageRate = 0.1f;
    private float damage = 1.0f;



    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        var enemy = other.gameObject.GetComponent<EnemyBase>();
        if (enemy != null) {
            other.transform.GetComponent<EnemyBase>().TakeDamage(damage);
            Player.CurStamina += StaminaGainFromEnemy;
        }
        else if (other.tag == "Crate") {
            Destroy(other.gameObject);
            Player.RandomWeapon();

        }
        else if (other.tag == "Switch") {
            other.GetComponent<SwitchScript>().HitLever();
        }
        else if (other.tag == "Shop") {
            if (Vector3.Distance(Player.transform.position, other.transform.position) < 20) {
                var weapon = other.GetComponent<WeaponBuyScript>();
                var health = other.GetComponent<HealthUpgrade>();
                var stamina = other.GetComponent<StaminaUpgrade>();
                if (weapon != null && !weapon.Purchased) {
                    weapon.Purchased = Player.UnlockWeapon(weapon.WeaponNumber, weapon.GemCount);
                }
                if (health != null) {
                    if (health.Purchasable && Player.GiveHealth(health.Amount, health.GemCount))
                        health.Purchased();
                }
                if (stamina != null) {
                    if (stamina.Purchasable && Player.GiveStamina(stamina.Amount, stamina.GemCount))
                        stamina.Purchased();
                }

            }
            else {
                //Have some error message show. Stops pistols from buying
            }
        }
        else if (other.tag == "Gems") {
            Player.gemCount++;
            Destroy(other.gameObject);
        }
    }
}
