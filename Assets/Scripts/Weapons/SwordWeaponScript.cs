using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SwordWeaponScript : BaseWeaponScript {
    private bool AttackedLeft;

    public override float Attack() {
        if (_attacking)
            return 0;

        Debug.Log(chargePercentage);

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce * (1 + 3 * chargePercentage), ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        chargePercentage = 0;
        if (AttackedLeft) {
            GetComponent<Animation>().Stop("SwordAttack1");
            GetComponent<Animation>().Play("SwordAttack1");
            AttackedLeft = false;
        }
        else {
            GetComponent<Animation>().Stop("SwordAttack2");
            GetComponent<Animation>().Play("SwordAttack2");
            AttackedLeft = true;
        }
        return staminaUse;
    }
}
