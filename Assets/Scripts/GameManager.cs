using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    private float maxComboTime = 3.0f;
    public int currentCombo;
    public float enemyDeathTime;
    public bool newEnemyDeath;

    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        currentCombo = 0;
        enemyDeathTime = -3242094309823;
        newEnemyDeath = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        ComboTracker();
    }

    public void ComboTracker() {
        if (Time.time > enemyDeathTime + maxComboTime) {
            currentCombo = 0;

        } else if (newEnemyDeath && Time.time <= enemyDeathTime + maxComboTime) {
            currentCombo += 1;
            newEnemyDeath = false;
        }



    }

}
