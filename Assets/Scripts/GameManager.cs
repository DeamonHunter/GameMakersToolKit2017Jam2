using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public LevelController level;
    public float maxComboTime = 4.0f;
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
    public Text PauseText;
    public Text PauseTextSmall;
    public bool Paused;


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
        if (Paused && Input.GetMouseButtonDown(0)) {
            PauseText.enabled = false;
            PauseTextSmall.enabled = false;
            gameOverImage.GetComponent<FadeInScript>().ResetProgress();
            gameOverImage.SetActive(false);
            Paused = false;
            Time.timeScale = 1;
        }


        if (Input.GetKeyDown(KeyCode.P) && player != null)
            player.GetComponent<PlayerController>().gemCount += 500;

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

        if (PlayerController.playerDead && !gameOverText.enabled) {
            StartCoroutine("RestartGame");
            SlowDown();
            gameOverText.enabled = true;
            gameOverImage.SetActive(true);

        }
        else if (!PlayerController.playerDead && Input.GetKeyDown(KeyCode.Escape)) {
            if (!Paused) {
                PauseText.enabled = true;
                PauseTextSmall.enabled = true;
                gameOverImage.SetActive(true);
                Paused = true;
                Time.timeScale = 0;
            }
            else
                SceneManager.LoadScene(0);
        }
        //Debug.Log(comboTimeRemaining);
    }

    public void ComboTracker() {
        comboTimeRemaining += 3.0f;

    }

    private IEnumerator RestartGame() {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(0);
    }

    private void SlowDown() {
        if (Time.timeScale - 0.05 < 0)
            Time.timeScale = 0;
        else {
            Time.timeScale -= 0.05f;
            Invoke("SlowDown", 0.033f);
        }
    }
}
