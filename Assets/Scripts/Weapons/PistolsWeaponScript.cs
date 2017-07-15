using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PistolsWeaponScript : BaseWeaponScript {
    public GameObject Bullet;
    public GameObject GunLeft, GunRight;

    private bool LeftSideAttacked;

    public override float Attack() {
        if (_attacking)
            return 0;

        var pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        if (LeftSideAttacked) {
            GetComponent<Animation>().Stop("PistolRightAttack");
            GetComponent<Animation>().Play("PistolRightAttack");
            var bullet = Instantiate(Bullet, GunRight.transform.position, GunRight.transform.rotation).GetComponent<PlayerProjectile>();
            bullet.MoveDirection = new Vector2(pos.x, pos.y).normalized;
            bullet.transform.position += new Vector3(bullet.MoveDirection.x, bullet.MoveDirection.y) * 2;
            bullet.staminaGain = StaminaGainFromEnemy;
            LeftSideAttacked = false;
        }
        else {
            GetComponent<Animation>().Stop("PistolLeftAttack");
            GetComponent<Animation>().Play("PistolLeftAttack");
            var bullet = Instantiate(Bullet, GunLeft.transform.position, GunLeft.transform.rotation).GetComponent<PlayerProjectile>();
            bullet.MoveDirection = new Vector2(pos.x, pos.y).normalized;
            bullet.transform.position += new Vector3(bullet.MoveDirection.x, bullet.MoveDirection.y) * 2;
            bullet.staminaGain = StaminaGainFromEnemy;
            LeftSideAttacked = true;
        }
        return staminaUse;
    }




}
