using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class SpearWeaponScript : MonoBehaviour {

    public Vector2 StartOffset;
    public GameObject Player;
    public float AttackVelocity;
    public float Deceleration;

    private Vector2 moveDirection;
    private float movespeed;
    private bool attacked;

    // Use this for initialization
    void Start() {
        Player = transform.parent.gameObject;
        transform.localPosition = StartOffset;
    }

    void Update() {
        //Player.transform.position = Player.transform.position + moveDirection * movespeed * Time.deltaTime;
        if (attacked) {
            if (movespeed < 0.1)
                attacked = false;
            else {
                var hit = Physics2D.Raycast(transform.position, moveDirection, movespeed * Time.deltaTime);
                if (hit.collider != null) {
                    Player.transform.position += new Vector3(moveDirection.x, moveDirection.y) * movespeed * Time.deltaTime;
                    movespeed -= Deceleration * Time.deltaTime;
                }
            }
        }
    }

    public void Attack() {
        Debug.Log("Attacking");
        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        moveDirection = pos.normalized;
        attacked = true;
        movespeed = AttackVelocity;
    }
}
