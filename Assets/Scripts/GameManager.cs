using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public float maxComboTime = 8.0f;
    public float comboTimeRemaining;

    public int Combo {
        get { return combo; }
        set {
            combo = value;
            ComboTracker();
        }
    }

    private int combo;
    int timeToAdd;
    public Text gameOverText;
    public GameObject gameOverImage;


    void Awake() {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        comboTimeRemaining = 0;
        combo = 0;
        gameOverText.text = "GAME OVER";
        gameOverText.enabled = false;
        gameOverImage.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (comboTimeRemaining > maxComboTime) {
            comboTimeRemaining = maxComboTime;
        }
        else if (comboTimeRemaining > 0) {
            comboTimeRemaining -= Time.deltaTime;
        }
        else if (comboTimeRemaining <= 0) {
            comboTimeRemaining = 0;
            combo = 0;
        }

        if (PlayerController.playerDead) {
            Invoke("RestartGame", 3);
            gameOverText.enabled = true;
            gameOverImage.SetActive(true);

        }
        //Debug.Log(comboTimeRemaining);
    }

    public void ComboTracker() {
        comboTimeRemaining += 3.0f;

    }

    private void RestartGame() {
        SceneManager.LoadScene(0);
    }
}
