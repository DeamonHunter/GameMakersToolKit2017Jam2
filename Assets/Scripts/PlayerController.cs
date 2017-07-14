﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private SpearWeaponScript weapon;
    public Vector3 HalfScreenSize;
    public Slider slider;

    public float MaxStamina;
    public float Speed;
    public float StaminaDepletionRate;
    public float StaminaGainRate;
    private float curStamina;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<SpearWeaponScript>();
        curStamina = MaxStamina;
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

        if (Input.GetMouseButton(0))
            weapon.Attack();
    }

    private void Movement() {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.1) {
            if (curStamina > 0) {
                //Debug.Log(curStamina + ":" + Mathf.Log(curStamina / MaxStamina + 1));
                transform.position += new Vector3(movement.x, movement.y) * Speed * Mathf.Log(curStamina / MaxStamina + 1) * Time.deltaTime;

                curStamina -= StaminaDepletionRate * Time.deltaTime;
            }
        }
        else {
            if (curStamina < MaxStamina)
                curStamina += Time.deltaTime * StaminaGainRate;
            else
                curStamina = MaxStamina;
        }
    }
}
