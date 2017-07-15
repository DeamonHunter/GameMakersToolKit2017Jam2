﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : EnemyBase {
    // Update is called once per frame
    protected override void Update() {
        Movement();
    }

    private void Movement() {
        if (Activated && GameManager.instance.player != null) {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, moveSpeed *
            Time.deltaTime);
        }
    }
}
