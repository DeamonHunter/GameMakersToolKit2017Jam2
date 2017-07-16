using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour {

    float moveSpeed = 5.0f;
    public Enemy_4 enemy;

    // Use this for initialization
    void Start() {
        enemy = gameObject.GetComponentInChildren<Enemy_4>();
    }

    // Update is called once per frame
    void Update() {
        if (enemy.Activated) {
            transform.Rotate(0, 0, 50 * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, moveSpeed *
                                                                                                                         Time.deltaTime);
        }
    }

}
