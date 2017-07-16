﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour {
    public DoorScript[] Doors;
    public Transform[] LevelCentres;
    public GameObject[] LevelDesigns;
    public GameObject[] Enemies;
    public List<int[]> WaveEnemies;
    public Vector2 RoomSize;
    public ArrowScript Arrow;

    public bool LevelDone;
    public bool DoorClosed;

    private int waveCount;
    private SwitchScript switchScript;
    //private List<GameObject> SpawnedEnemies;
    private int CurrentDoor;
    private GameObject currentDesign;
    public GameObject EnemyParent;

    // Use this for initialization
    void Start() {
        //SpawnedEnemies = new List<GameObject>();
        switchScript = GetComponentInChildren<SwitchScript>();
        LevelDone = true;
        AuthoredWaves();
    }

    // Update is called once per frame
    void Update() {
        if (!LevelDone && EnemyParent.transform.childCount == 0)
            FinishLevel();
        if (!DoorClosed) {
            Arrow.SetDirection(CurrentDoor, LevelDone);
        }
        else {
            Arrow.ShowArrow = false;
        }
    }

    public void SpawnLevel() {
        for (int i = EnemyParent.transform.childCount - 1; i >= 0; i--) {
            Destroy(EnemyParent.transform.GetChild(i));
        }
        CurrentDoor = Random.Range(0, 3);
        Doors[CurrentDoor].DoorOpen = true;
        try {
            var wave = WaveEnemies[waveCount];
            for (int i = 0; i < wave.Length; i++) {
                for (int j = 0; j < wave[i]; j++) {
                    Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                    Instantiate(Enemies[i], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
                }
            }
        }
        catch (Exception) {
            Debug.Log("Failed to find wave. Spawning random.");
            for (int i = 0; i < waveCount / 4 + 5; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[0], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
            }
            for (int i = 0; i < waveCount / 6 + 2; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[1], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
            }
            for (int i = 0; i < waveCount / 3 + 3; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[2], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
            }
            for (int i = 0; i < waveCount / 5 + 2; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[3], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
            }
            for (int i = 0; i < 2; i++) {
                Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                Instantiate(Enemies[4], LevelCentres[CurrentDoor].position + rand, Quaternion.identity, EnemyParent.transform);
            }

        }
        currentDesign = Instantiate(LevelDesigns[Random.Range(0, LevelDesigns.Length)], LevelCentres[CurrentDoor].position, Quaternion.identity);
        float degrees;
        switch (CurrentDoor) {
            case 0:
                degrees = 90;
                break;
            case 1:
                degrees = 0;
                break;
            case 2:
                degrees = -90;
                break;
            case 3:
                degrees = -180;
                break;
            default:
                degrees = 0;
                break;
        }
        currentDesign.transform.rotation = Quaternion.Euler(0, 0, degrees);
        LevelDone = false;
        DoorClosed = false;
    }

    public void StartLevel() {
        for (int i = 0; i < EnemyParent.transform.childCount; i++) {
            if (EnemyParent.transform.GetChild(i).name == "Enemy_4(Clone)") {
                var basic = EnemyParent.transform.GetChild(i).GetComponentInChildren<EnemyBase>();
                if (basic != null)
                    basic.ActivateEnemy();
            }
            else {

                var basic = EnemyParent.transform.GetChild(i).GetComponent<EnemyBase>();
                if (basic != null)
                    basic.ActivateEnemy();
            }
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

    public void AuthoredWaves() {
        if (WaveEnemies == null)
            WaveEnemies = new List<int[]>();
        WaveEnemies.Add(new[] { 10, 0, 0, 0, 0 });
        WaveEnemies.Add(new[] { 10, 1, 0, 0, 2 });
        WaveEnemies.Add(new[] { 10, 2, 3, 0, 2 });
        WaveEnemies.Add(new[] { 10, 0, 20, 0, 2 });
        WaveEnemies.Add(new[] { 10, 0, 5, 0, 2 });
        WaveEnemies.Add(new[] { 5, 0, 0, 3, 2 });
        WaveEnemies.Add(new[] { 5, 2, 0, 5, 2 });
        WaveEnemies.Add(new[] { 10, 0, 0, 10, 2 });
        WaveEnemies.Add(new[] { 20, 2, 0, 0, 2 });
        WaveEnemies.Add(new[] { 10, 1, 10, 3, 2 });
        WaveEnemies.Add(new[] { 10, 2, 10, 5, 2 });
        WaveEnemies.Add(new[] { 15, 3, 10, 3, 2 });
        WaveEnemies.Add(new[] { 10, 2, 3, 3, 2 });
        WaveEnemies.Add(new[] { 20, 3, 10, 10, 2 });
        WaveEnemies.Add(new[] { 20, 5, 20, 10, 2 });

    }
}
