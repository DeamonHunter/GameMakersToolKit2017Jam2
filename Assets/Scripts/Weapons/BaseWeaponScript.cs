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
        Debug.Log("Entered");
        var enemy = other.gameObject.GetComponent<EnemyBase>();
        if (enemy != null) {
            Destroy(other.gameObject);
            Player.CurStamina += StaminaGainFromEnemy;
        }
        else if (other.tag == "Crate") {
            Destroy(other.gameObject);
            Player.RandomWeapon();

        }
    }
}
