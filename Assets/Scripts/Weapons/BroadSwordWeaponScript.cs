﻿using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class BroadSwordWeaponScript : BaseWeaponScript {

    public override float Attack() {
        if (_attacking)
            return 0;

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce * (1 + 3 * chargePercentage), ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        chargePercentage = 0;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        return staminaUse;
    }
}
