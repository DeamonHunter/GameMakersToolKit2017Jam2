using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private BaseWeaponScript weapon;
    public Vector3 HalfScreenSize;
    public Slider slider;

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
    private float curHealth;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<BaseWeaponScript>();
        curStamina = MaxStamina;
        curHealth = MaxHealth;
        HalfScreenSize = new Vector3(Screen.width, Screen.height) / 2;
    }

    // Update is called once per frame
    void Update() {
        Movement();
        //Debug.Log(curStamina);
        slider.value = curStamina / MaxStamina;
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
}
