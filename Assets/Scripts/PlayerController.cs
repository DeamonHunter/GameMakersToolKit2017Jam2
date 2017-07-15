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

    public float MaxHealth;
    public float MaxStamina;
    public float Speed;
    public float StaminaDepletionRate;
    public float StaminaGainRate;
    public float StaminaCooldownPeriod;

    public float CurStamina {
        get { return curStamina; }
        set {
            curStamina = value;
            staminaCooldown = Time.time + StaminaCooldownPeriod;
        }
    }

    private float staminaCooldown;
    private float curStamina;
    public float curHealth;

    public int gemCount;
    private float gemCollectTime;
    private float gemCollectRate = 0.1f;

    public bool[] Weapons;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        weaponsPurchased.Add(0);
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

        if (Input.GetMouseButton(0) && weapon.staminaUse < CurStamina)
            CurStamina -= weapon.Attack();

        RegainStamina();


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
        curHealth -= damage;
        if (curHealth <= 0) {
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
                return true;
            }
            else {
                //Another error message
            }
        }
        else {
            //Give error message
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Gems" && Time.time > gemCollectTime) {
            gemCount += 1;
            gemCollectTime = Time.time + gemCollectRate;
            Destroy(other.gameObject);

        }
    }


}
