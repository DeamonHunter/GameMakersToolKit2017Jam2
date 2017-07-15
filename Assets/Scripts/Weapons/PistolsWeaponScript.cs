using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PistolsWeaponScript : BaseWeaponScript {

    public override float Attack() {
        if (_attacking)
            return 0;

        Debug.Log("Attacking");

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        return staminaUse;
    }




}
