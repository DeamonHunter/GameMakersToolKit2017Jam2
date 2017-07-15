using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public DoorScript[] Doors;
    public Transform[] LevelCentres;
    public GameObject[] LevelDesigns;
    public GameObject[] Enemies;

    private List<GameObject> SpawnedEnemies;
    private int CurrentDoor;

    // Use this for initialization
    void Start() {
        SpawnedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (SpawnedEnemies.Count == 0) {
            CurrentDoor = Random.Range(0, 3);
            Doors[CurrentDoor].DoorOpen = true;
            SpawnedEnemies.Add(Instantiate(Enemies[0], Vector3.zero, Quaternion.identity));
        }
    }
}
