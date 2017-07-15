using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class ShottyWeaponScript : BaseWeaponScript {
    public GameObject Bullet;
    public int NumberOfBullets;
    public float Spread;
    public float MaxDelay;

    private bool LeftSideAttacked;

    private Vector2 pos;

    public override float Attack() {
        if (_attacking)
            return 0;

        pos = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
        Player.GetComponent<Rigidbody2D>().AddForce(pos.normalized * AttackForce, ForceMode2D.Impulse);
        _lastAttack = Time.time;
        _attacking = true;
        for (int i = 0; i < NumberOfBullets; i++) {
            Invoke("SpawnBullet", Random.Range(0, MaxDelay));
        }
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play();
        return staminaUse;
    }

    private void SpawnBullet() {
        var bullet = Instantiate(Bullet, transform.position, transform.rotation).GetComponent<PlayerProjectile>();
        var direction = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        var bulletDirection = direction + Random.Range(-Spread, Spread);

        bullet.MoveDirection = new Vector2(Mathf.Cos(bulletDirection * Mathf.Deg2Rad), Mathf.Sin(bulletDirection * Mathf.Deg2Rad)).normalized;
        bullet.staminaGain = StaminaGainFromEnemy;

    }
}

