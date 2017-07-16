using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class BroadSwordWeaponScript : BaseWeaponScript {

    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    int soundNumber;

    public override float Attack() {
        if (_attacking)
            return 0;

        soundNumber = Random.Range(0, 2);
        if (soundNumber == 0)
            Instantiate(attackSound1, transform.position, transform.rotation);
        if (soundNumber == 1)
            Instantiate(attackSound2, transform.position, transform.rotation);
        if (soundNumber == 2)
            Instantiate(attackSound3, transform.position, transform.rotation);
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
