using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SwordWeaponScript : BaseWeaponScript {
    private bool AttackedLeft;

    public AudioSource swordAttack;
    public AudioSource swordAttack2;
    public AudioSource swordAttack3;
    public AudioSource swordAttack4;

    public override float Attack() {
        if (_attacking)
            return 0;

        int soundNumber = Random.Range(0, 3);
        if (soundNumber == 0)
            Instantiate(swordAttack, transform.position, transform.rotation);
        if (soundNumber == 1)
            Instantiate(swordAttack2, transform.position, transform.rotation);
        if (soundNumber == 2)
            Instantiate(swordAttack3, transform.position, transform.rotation);
        if (soundNumber == 3)
            Instantiate(swordAttack4, transform.position, transform.rotation);

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
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
