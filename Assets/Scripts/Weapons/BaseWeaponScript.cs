using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponScript : MonoBehaviour {
    public Vector2 StartOffset;
    public float StartRotation;
    public PlayerController Player;
    public float AttackForce;
    public float AttackTimeout;

    public bool UseHeat; //When using guns

    public float staminaUse;
    public float StaminaGainFromEnemy;

    protected bool _attacking;
    protected float _lastAttack;

    //private float damageTime;
    //private float damageRate = 0.1f;
    private float damage = 1.0f;



    // Use this for initialization
    void Start() {
        Player = transform.parent.gameObject.GetComponent<PlayerController>();
        transform.localPosition = StartOffset;
        transform.localRotation = Quaternion.Euler(0, 0, StartRotation);
    }

    void Update() {
        if (_attacking && _lastAttack + AttackTimeout < Time.time) {
            _attacking = false;
        }
    }

    public abstract float Attack();

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Entered");
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
                if (weapon != null && !weapon.Purchased) {

                    weapon.Purchased = Player.UnlockWeapon(weapon.WeaponNumber, weapon.GemCount);
                }

            }
            else {
                //Have some error message show. Stops pistols from buying
            }
        }

    }
}
