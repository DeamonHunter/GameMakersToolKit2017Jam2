using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class LauncherWeaponScript : BaseWeaponScript {
    public GameObject Bullet;
    public float AdditionalSize;
    public float AdditionalForce;
    public float SmallestSize;

    private bool LeftSideAttacked;

    public override float Attack() {
        if (_attacking)
            return 0;

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * (AttackForce + chargePercentage * AdditionalForce), ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        var bullet = Instantiate(Bullet, transform.position, transform.rotation).GetComponent<PlayerProjectile>();
        bullet.MoveDirection = new Vector2(pos.x, pos.y).normalized;
        bullet.transform.position += pos.normalized * 4 * chargePercentage;
        bullet.staminaGain = StaminaGainFromEnemy;
        bullet.transform.localScale = new Vector3(SmallestSize + chargePercentage * AdditionalSize, SmallestSize + chargePercentage * AdditionalSize, 1);
        bullet.lifeTime = 1;
        chargePercentage = 0;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        return staminaUse;
    }

}

