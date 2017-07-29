using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour {
    public LevelController LevelController;
    public Vector2 Force;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            if (!LevelController.WaveStarted && LevelController.WaveSpawned) {
                LevelController.CloseDoor();
                LevelController.StartLevel();
                other.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
            }
    }
}
