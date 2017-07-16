using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PistolWeaponScript : BaseWeaponScript {
    public GameObject Bullet;
    public AudioSource pistolSound;

    public override float Attack() {
        if (_attacking)
            return 0;
        Instantiate(pistolSound, transform.position, transform.rotation);
        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        var bullet = Instantiate(Bullet, transform.position, transform.rotation).GetComponent<PlayerProjectile>();
        bullet.MoveDirection = new Vector2(pos.x, pos.y).normalized;
        bullet.transform.position += new Vector3(bullet.MoveDirection.x, bullet.MoveDirection.y) * 2;
        bullet.staminaGain = StaminaGainFromEnemy;

        return staminaUse;
    }




}
