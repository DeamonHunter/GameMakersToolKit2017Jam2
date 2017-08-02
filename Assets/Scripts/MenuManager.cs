using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public RawImage img;
    public Button back;

    public void Start() {
        img.GetComponent<RawImage>();
        img.enabled = false;
        Time.timeScale = 1;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            img.enabled = false;
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);

    }

    public void ExitGame() {
        Application.Quit();
    }

    public void Controls() {
        img.enabled = true;
    }



}
