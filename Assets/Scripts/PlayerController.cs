using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    public Slider slider;

    public float MaxStamina;
    public float Speed;
    public float StaminaDepletionRate;
    public float StaminaGainRate;
    private float curStamina;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        curStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Debug.Log(curStamina);
        slider.value = curStamina / MaxStamina;
    }

    private void Movement() {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.1) {
            if (curStamina > 0) {
                //Debug.Log(curStamina + ":" + Mathf.Log(curStamina / MaxStamina + 1));
                rb.velocity = movement * Speed * (Mathf.Log(curStamina / MaxStamina + 1));

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
