using UnityEngine;

public class SpearWeaponScript : MonoBehaviour {

    public Vector2 StartOffset;
    public GameObject Player;
    public float AttackForce;
    public float AttackTimeout;
    public float staminaUse;

    private bool _attacking;
    private float _lastAttack;


    // Use this for initialization
    void Start() {
        Player = transform.parent.gameObject;
        transform.localPosition = StartOffset;
    }

    void Update() {
        if (_attacking && _lastAttack + AttackTimeout < Time.time) {
            _attacking = false;
        }
    }

    public float Attack() {
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

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Entered");
        if (other.tag == "Enemy")
            Destroy(other.gameObject);
    }
}
