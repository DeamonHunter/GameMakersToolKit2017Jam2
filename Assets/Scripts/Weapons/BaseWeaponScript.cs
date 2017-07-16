using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponScript : MonoBehaviour {
    public Vector2 StartOffset;
    public float StartRotation;
    public PlayerController Player;
    public GameObject gem;
    public GameObject CrateFlyAway;
    public GameObject Energy;
    public float AttackForce;
    public float AttackTimeout;

    public bool Charger; //When using guns
    public bool Invunerable;
    public float TimeToCharge;
    public float chargePercentage;

    public float staminaUse;
    public float AdditionalStaminaUse;
    public float StaminaGainFromEnemy;

    protected bool _attacking;
    protected float _lastAttack;

    private float damage = 1.0f;

    public AudioSource gemCollectSound;
    public AudioSource crateSmashSound;
    public AudioSource energyCollectSound;


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
            for (int i = 0; i < StaminaGainFromEnemy; i++) {
                Instantiate(Energy, transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f)), transform.rotation);
            }
        }
        else if (other.tag == "Crate") {
            for (int j = 0; j < 7; j++) {
                float xGem = Random.Range(-4.0f, 4.0f);
                float yGem = Random.Range(-4.0f, 4.0f);
                Vector3 gemSpawn = new Vector3(xGem, yGem);
                Instantiate(gem, other.transform.position + gemSpawn, other.transform.rotation);
            }
            Instantiate(CrateFlyAway, other.transform.position, other.transform.rotation);
            GameManager.instance.Combo *= 2;
            Instantiate(crateSmashSound, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Player.RandomWeapon();

        }
        else if (other.tag == "Switch") {
            other.GetComponent<SwitchScript>().HitLever();
        }
        else if (other.tag == "Shop") {
            if (Vector3.Distance(Player.transform.position, other.transform.position) < 20) {
                var weapon = other.GetComponent<WeaponBuyScript>();
                var health = other.GetComponent<HealUpgrade>();
                var maxHealth = other.GetComponent<MaxHealthUpgrade>();
                var stamina = other.GetComponent<StaminaUpgrade>();
                if (weapon != null && !weapon.Purchased) {
                    weapon.Purchased = Player.UnlockWeapon(weapon.WeaponNumber, weapon.GemCount);
                }
                if (health != null) {
                    if (health.Purchasable && Player.GiveHealth(health.GemCount))
                        health.Purchased();
                }

                if (maxHealth != null) {
                    if (maxHealth.Purchasable && Player.GiveMaxHealth(maxHealth.Amount, maxHealth.GemCount))
                        maxHealth.Purchased();
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
            Instantiate(gemCollectSound, transform.position, transform.rotation);
            Player.gemCount++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Energy") {
            Instantiate(energyCollectSound, transform.position, transform.rotation);
            Player.CurStamina += 5;
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
