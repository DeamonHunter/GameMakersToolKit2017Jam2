using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public float maxComboTime = 21.0f;
    public float comboTimeRemaining;
    int timeToAdd;
    public Text gameOverText;
    

    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        comboTimeRemaining = 0;
        gameOverText.text = "GAME OVER";
        gameOverText.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (comboTimeRemaining > 21.0f) {
            comboTimeRemaining = 21.0f;
        } else if (comboTimeRemaining > 0) {
            comboTimeRemaining -= Time.deltaTime;
        } else if (comboTimeRemaining <= 0) {
            comboTimeRemaining = 0;
        }

        if (PlayerController.playerDead) {
            Invoke("RestartGame", 3);
            gameOverText.enabled = true;

        }
    }

    public void ComboTracker(float timeToAdd) {
        comboTimeRemaining += 3.0f;

    }

    private void RestartGame() {
        SceneManager.LoadScene(0);
    }
}
