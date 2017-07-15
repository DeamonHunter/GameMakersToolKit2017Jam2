using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyScript : MonoBehaviour {
    public int WeaponNumber;
    public int GemCount;

    public GameObject gemSprite;
    public GameObject UnlockedSprite;
    public bool Purchased;

    private TextMesh text;
    private GameObject instancedGem;

    // Use this for initialization
    void Start() {
        instancedGem = Instantiate(gemSprite, transform.position + new Vector3(-2, -3.5f, -0.02f), transform.rotation, transform);
        text = GetComponentInChildren<TextMesh>();
        text.text = GemCount.ToString();
    }

    void Update() {
        if (Purchased) {
            Instantiate(UnlockedSprite, transform.position + new Vector3(0, 0, -0.03f), transform.rotation, transform);
            instancedGem.SetActive(false);
            text.gameObject.SetActive(false);
        }

    }
}
