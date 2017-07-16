using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    Text gemText;
    public GameObject Enemies;

    // Use this for initialization
    void Start() {
        gemText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        gemText.text = Enemies.transform.childCount.ToString();
    }
}
