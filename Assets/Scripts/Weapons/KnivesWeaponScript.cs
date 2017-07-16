using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class KnivesWeaponScript : BaseWeaponScript {
    private bool LeftSideAttacked;
    public AudioSource attackSound;

    public override float Attack() {
        if (_attacking)
            return 0;

        Instantiate(attackSound, transform.position, transform.rotation);
        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        if (LeftSideAttacked) {
            GetComponent<Animation>().Stop("KnifeAttackRight");
            GetComponent<Animation>().Play("KnifeAttackRight");
            LeftSideAttacked = false;
        }
        else {
            GetComponent<Animation>().Stop("KnifeAttackLeft");
            GetComponent<Animation>().Play("KnifeAttackLeft");
            LeftSideAttacked = true;
        }
        return staminaUse;
    }




}
