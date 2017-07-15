﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public DoorScript[] Doors;
    public Transform[] LevelCentres;
    public GameObject[] LevelDesigns;
    public GameObject[] Enemies;
    public Vector2 RoomSize;

    public bool LevelDone;
    public bool DoorClosed;

    private int waveCount;
    private SwitchScript switchScript;
    private List<GameObject> SpawnedEnemies;
    private int CurrentDoor;
    private GameObject currentDesign;

    // Use this for initialization
    void Start() {
        SpawnedEnemies = new List<GameObject>();
        switchScript = GetComponentInChildren<SwitchScript>();
        LevelDone = true;
    }

    // Update is called once per frame
    void Update() {
        SpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        if (!LevelDone && SpawnedEnemies.Count <= 0)
            FinishLevel();
    }

    public void SpawnLevel() {
        if (SpawnedEnemies.Count == 0) {
            CurrentDoor = Random.Range(0, 3);
            Doors[CurrentDoor].DoorOpen = true;
            for (int i = 0; i <= waveCount; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[0], LevelCentres[CurrentDoor].position + rand, Quaternion.identity);
            }
            currentDesign = Instantiate(LevelDesigns[Random.Range(0, LevelDesigns.Length)], LevelCentres[CurrentDoor].position, Quaternion.identity);
            LevelDone = false;
            DoorClosed = false;
        }
        else {
            Debug.Log("Tried to spawn level while enemies were still alive.");
        }
    }

    public void StartLevel() {
        foreach (var enemy in SpawnedEnemies) {
            enemy.GetComponent<EnemyBase>().ActivateEnemy();
        }
    }

    public void FinishLevel() {
        Doors[CurrentDoor].DoorOpen = true;
        LevelDone = true;
        DoorClosed = false;
        switchScript.LeverHit = false;
        waveCount++;
    }

    public void CloseDoor() {
        Doors[CurrentDoor].DoorOpen = false;
        DoorClosed = true;
    }

    public void DestroyDesign() {
        Destroy(currentDesign);
    }
}
