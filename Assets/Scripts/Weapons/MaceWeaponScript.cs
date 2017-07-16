using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class MaceWeaponScript : BaseWeaponScript {

    private bool AttackedLeft;
    public AudioSource maceSound;

    public override float Attack() {
        if (_attacking)
            return 0;
        Instantiate(maceSound, transform.position, transform.rotation);
        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce * (1 + 2 * chargePercentage), ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        chargePercentage = 0;
        if (AttackedLeft) {
            GetComponent<Animation>().Stop("MaceAttack1");
            GetComponent<Animation>().Play("MaceAttack1");
            AttackedLeft = false;
        }
        else {
            GetComponent<Animation>().Stop("MaceAttack2");
            GetComponent<Animation>().Play("MaceAttack2");
            AttackedLeft = true;
        }
        return staminaUse;
    }
}
