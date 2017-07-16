using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponScript : MonoBehaviour {
    public Vector2 StartOffset;
    public float StartRotation;
    public PlayerController Player;
    public GameObject gem;
    public float AttackForce;
    public float AttackTimeout;

    public bool Charger; //When using guns
    public bool Invunerable;
    public float TimeToCharge;
    public float chargePercentage;

    public float staminaUse;
    public float StaminaGainFromEnemy;

    protected bool _attacking;
    protected float _lastAttack;

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
            for (int j = 0; j < 7; j++) {
                int xGem = Random.Range(1, 4);
                int yGem = Random.Range(1, 4);
                Vector3 gemSpawn = new Vector3(xGem, yGem);
                Instantiate(gem, transform.position + gemSpawn, transform.rotation);
            }
            GameManager.instance.Combo *= 2;
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

    public void IncreaseCharge() {
        if (_attacking)
            return;
        chargePercentage += Time.deltaTime / TimeToCharge;
        if (chargePercentage > 1)
            chargePercentage = 1;
    }
}
