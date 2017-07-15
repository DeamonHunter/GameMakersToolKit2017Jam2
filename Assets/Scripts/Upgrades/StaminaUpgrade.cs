using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUpgrade : MonoBehaviour {
    public int GemCount;
    public int Amount;
    public GameObject gemSprite;
    public GameObject UnlockedSprite;

    public bool Purchasable {
        get { return Time.time > timeLock; }
    }

    private TextMesh text;
    private GameObject instancedBlock;
    private float timeLock;

    // Use this for initialization
    void Start() {
        Instantiate(gemSprite, transform.position + new Vector3(-2.5f, -5.2f, 0f), transform.rotation, transform);
        instancedBlock = Instantiate(UnlockedSprite, transform.position + new Vector3(0, 0, -0.01f), transform.rotation, transform);
        text = GetComponentInChildren<TextMesh>();
    }

    void Update() {
        text.text = GemCount.ToString();
        if (Time.time < timeLock)
            instancedBlock.SetActive(true);
        else
            instancedBlock.SetActive(false);
    }

    public void Purchased() {
        GemCount += 10;
        timeLock = Time.time + 1;
    }

}
