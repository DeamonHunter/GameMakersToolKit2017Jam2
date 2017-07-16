using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private BaseWeaponScript weapon;
    public Vector3 HalfScreenSize;

    public GameObject[] ChooseableWeapons;
    private List<int> weaponsPurchased = new List<int>();
    private GameObject curWeapon;
    public ChargeBarScript ChargeBar;
    public ShopMessageScript shopMessage;

    public float MaxHealth;
    public float MaxStamina;
    public float Speed;
    public float StaminaDepletionRate;
    public float StaminaGainRate;
    public float StaminaCooldownPeriod;

    public AudioSource upgradeSound;
    public AudioSource gemCollectSound;

    public float CurStamina {
        get { return curStamina; }
        set {
            if (value > curStamina) {
                if (value > MaxStamina)
                    curStamina = MaxStamina;
                else
                    curStamina = value;
            }
            else {
                if (value < 0)
                    curStamina = 0;
                else
                    curStamina = value;
                staminaCooldown = Time.time + StaminaCooldownPeriod;

            }
        }
    }

    private float staminaCooldown;
    private float curStamina;
    public float curHealth;

    public int gemCount;
    private bool charging;

    public bool[] Weapons;

    public static bool playerDead;

    // Use this for initialization
    void Start() {
        playerDead = false;
        rb = GetComponent<Rigidbody2D>();
        weaponsPurchased.Add(6);
        RandomWeapon();
        curStamina = MaxStamina;
        curHealth = MaxHealth;
        HalfScreenSize = new Vector3(Screen.width, Screen.height) / 2;
        gemCount = 100;

    }

    // Update is called once per frame
    void Update() {
        Movement();
        //Debug.Log(curStamina);
        var pos = Input.mousePosition - HalfScreenSize;
        var angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Attack();

        RegainStamina();

        ChargeBar.transform.position = transform.position + new Vector3(0, 4, 0);

    }

    private void Attack() {
        if (weapon.Charger) {
            if (Input.GetMouseButtonDown(0))
                charging = true;
            if (charging) {
                if (weapon.chargePercentage > 0.01)
                    ChargeBar.gameObject.SetActive(true);
                ChargeBar.Percantage = weapon.chargePercentage;
                weapon.IncreaseCharge();
            }
            else {
                ChargeBar.gameObject.SetActive(false);
                charging = false;
            }
            if (Input.GetMouseButtonUp(0)) {
                curStamina -= weapon.Attack();
                charging = false;
            }
        }
        else {
            ChargeBar.gameObject.SetActive(false);
            if (Input.GetMouseButton(0) && weapon.staminaUse < CurStamina)
                CurStamina -= weapon.Attack();
        }
    }

    private void Movement() {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.1) {
            if (curStamina > 0) {
                //Debug.Log(curStamina + ":" + Mathf.Log(curStamina / MaxStamina + 1));
                //transform.position += new Vector3(movement.x, movement.y) * Speed * Mathf.Log(curStamina / MaxStamina + 1) * Time.deltaTime;
                rb.AddForce(movement * Speed * Mathf.Log(curStamina / MaxStamina + 1));
                CurStamina -= StaminaDepletionRate * Time.deltaTime;
            }
        }
    }

    private void RegainStamina() {
        if (Time.time < staminaCooldown)
            return;
        curStamina += Time.deltaTime * StaminaGainRate;
        if (curStamina > MaxStamina)
            curStamina = MaxStamina;
    }

    public void TakeDamage(float damage) {
        if (weapon.Invunerable)
            return;
        curHealth -= damage;
        if (curHealth <= 0) {
            playerDead = true;
            gameObject.SetActive(false);
        }
    }

    public void RandomWeapon() {
        if (curWeapon != null)
            Destroy(curWeapon);
        int rand = Random.Range(0, weaponsPurchased.Count);
        curWeapon = Instantiate(ChooseableWeapons[weaponsPurchased[rand]], transform.position, transform.rotation, transform);
        weapon = curWeapon.GetComponent<BaseWeaponScript>();
    }

    public bool UnlockWeapon(int weaponID, int weaponPrice) {
        if (weaponPrice <= gemCount) {
            if (!weaponsPurchased.Contains(weaponID)) {
                weaponsPurchased.Add(weaponID);
                gemCount -= weaponPrice;
                shopMessage.gameObject.SetActive(true);
                switch (weaponID) {
                    case 0:
                        Instantiate(upgradeSound, transform.position, transform.rotation);
                        shopMessage.Showtext("You have bought the Broadsword!");
                        break;
                    case 1:
                        Instantiate(upgradeSound, transform.position, transform.rotation);
                        shopMessage.Showtext("You have bought the Spear!");
                        break;
                    case 2:
                        Instantiate(upgradeSound, transform.position, transform.rotation);
                        shopMessage.Showtext("You have bought the Double Pistols!");
                        break;
                }
                return true;
            }
            else {
                shopMessage.gameObject.SetActive(true);
                shopMessage.Showtext("Already unlocked this item!");
            }
        }
        else {
            shopMessage.gameObject.SetActive(true);
            shopMessage.Showtext("Don't have enough gems to unlock this item!");
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Gems") {
            Instantiate(gemCollectSound, transform.position, transform.rotation);
            gemCount += 1;
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Energy") {
            CurStamina++;
            Destroy(other.gameObject);
        }
    }

    public bool GiveHealth(int Cost) {
        if (curHealth < MaxHealth) {
            if (gemCount >= Cost) {
                curHealth = MaxHealth;
                gemCount -= Cost;
                shopMessage.gameObject.SetActive(true);
                shopMessage.Showtext("Healed to full health!");
                return true;
            }
            shopMessage.gameObject.SetActive(true);
            shopMessage.Showtext("Don't have enough gems to heal!");
            return false;
        }
        shopMessage.gameObject.SetActive(true);
        shopMessage.Showtext("Already fully healed!");
        return false;
    }

    public bool GiveMaxHealth(int health, int Cost) {
        if (gemCount >= Cost) {
            MaxHealth += health;
            curHealth = MaxHealth;
            gemCount -= Cost;
            shopMessage.gameObject.SetActive(true);
            Instantiate(upgradeSound, transform.position, transform.rotation);
            shopMessage.Showtext("Have just bought " + health + " hp!");
            return true;
        }
        shopMessage.gameObject.SetActive(true);
        shopMessage.Showtext("Don't have enough gems to upgrade health!");
        return false;
    }

    public bool GiveStamina(int stamina, int Cost) {
        if (gemCount >= Cost) {
            MaxStamina += stamina;
            gemCount -= Cost;
            shopMessage.gameObject.SetActive(true);
            Instantiate(upgradeSound, transform.position, transform.rotation);
            shopMessage.Showtext("Have just bought " + stamina + " max stamina!");
            return true;
        }
        shopMessage.gameObject.SetActive(true);
        shopMessage.Showtext("Don't have enough gems to upgrade stamina!");
        return false;
    }

}
