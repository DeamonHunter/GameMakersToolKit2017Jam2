using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    private float maxComboTime = 3.0f;
    public float currentCombo;
    public float maxCombo;
    public float enemyDeathTime;
    public bool newEnemyDeath;
    public float comboTimeRemaining;
    int timeToAdd;

    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        currentCombo = 0;
        maxCombo = 10;
        enemyDeathTime = -3242094309823;
        newEnemyDeath = true;
        comboTimeRemaining = maxComboTime;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        comboTimeRemaining -= Time.deltaTime;
        Streak();
    }

    public void ComboTracker(float timeToAdd) {
        comboTimeRemaining += 3.0f;


    }

    public void Streak() {
        if (comboTimeRemaining <= 0) {
        currentCombo = 0;
    }
        else if (newEnemyDeath && Time.time <= enemyDeathTime + maxComboTime && currentCombo <= maxCombo) {
            currentCombo += 1;
            maxComboTime += 3.0f;
            newEnemyDeath = false;
        }

    }
