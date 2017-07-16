using System;
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
    private List<GameObject> SpawnedEnemies;
    private int CurrentDoor;
    private GameObject currentDesign;

    // Use this for initialization
    void Start() {
        SpawnedEnemies = new List<GameObject>();
        switchScript = GetComponentInChildren<SwitchScript>();
        LevelDone = true;
        AuthoredWaves();
    }

    // Update is called once per frame
    void Update() {
        SpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        SpawnedEnemies.AddRange(GameObject.FindGameObjectsWithTag("Crate"));
        if (!LevelDone && SpawnedEnemies.Count <= 0)
            FinishLevel();
        if (!DoorClosed) {
            Arrow.SetDirection(CurrentDoor, LevelDone);
        }
        else {
            Arrow.ShowArrow = false;
        }
    }

    public void SpawnLevel() {
        if (SpawnedEnemies.Count == 0) {
            CurrentDoor = Random.Range(0, 3);
            Doors[CurrentDoor].DoorOpen = true;
            try {
                var wave = WaveEnemies[waveCount];
                for (int i = 0; i < wave.Length; i++) {
                    for (int j = 0; j < wave[i]; j++) {
                        Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                        Instantiate(Enemies[i], LevelCentres[CurrentDoor].position + rand, Quaternion.identity);
                    }
                }
            }
            catch (Exception) {
                Debug.Log("Failed to find wave. Spawning random.");
                for (int i = 0; i < waveCount + 5; i++) {
                    Vector3 rand = new Vector3(Random.Range(-RoomSize.x, RoomSize.x), Random.Range(-RoomSize.y, RoomSize.y));
                    Instantiate(Enemies[0], LevelCentres[CurrentDoor].position + rand, Quaternion.identity);
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
        else {
            Debug.Log("Tried to spawn level while enemies were still alive.");
        }
    }

    public void StartLevel() {
        foreach (var enemy in SpawnedEnemies) {
            var basic = enemy.GetComponent<EnemyBase>();
            if (basic != null)
                basic.ActivateEnemy();
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
        WaveEnemies.Add(new[] { 20, 3, 10, 10, 2 });
        WaveEnemies.Add(new[] { 5, 0, 0, 0, 2 });
        WaveEnemies.Add(new[] { 0, 1, 0, 0, 2});
        WaveEnemies.Add(new[] { 5, 1, 0, 0, 2});
        WaveEnemies.Add(new[] { 5, 0, 5, 0, 2 });
        WaveEnemies.Add(new[] { 5, 0, 5, 0, 2 });
        WaveEnemies.Add(new[] { 5, 0, 0, 3, 2 });
        WaveEnemies.Add(new[] { 5, 2, 0, 5, 2 });
        WaveEnemies.Add(new[] { 10, 0, 0, 10, 2 });
        WaveEnemies.Add(new[] { 20, 2, 0, 0, 2});
        WaveEnemies.Add(new[] { 10, 1, 10, 3, 2});
        WaveEnemies.Add(new[] { 10, 2, 10, 5, 2});
        WaveEnemies.Add(new[] { 15, 3, 10, 3, 2});
        WaveEnemies.Add(new[] { 0, 5, 0, 0, 2});
        WaveEnemies.Add(new[] { 20, 3, 10, 10, 2});

    }
}
