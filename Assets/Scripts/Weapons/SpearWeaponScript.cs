﻿using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SpearWeaponScript : BaseWeaponScript {
    public override float Attack() {
        if (_attacking)
            return 0;

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce * (1 + 2f * chargePercentage), ForceMode2D.Impulse);
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        var charge = chargePercentage;
        chargePercentage = 0;
        return staminaUse + AdditionalStaminaUse * charge;
    }
}
