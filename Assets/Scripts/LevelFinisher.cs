﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinisher : MonoBehaviour {
    public LevelController LevelController;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            if (LevelController.LevelDone && !LevelController.DoorClosed)
                LevelController.CloseDoor();
    }
}
