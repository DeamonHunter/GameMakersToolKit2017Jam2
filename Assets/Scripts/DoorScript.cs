using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public bool DoorOpen;
    public float TimetoOpenDoor;

    public Color DoorOpenColor;
    public Color DoorClosedColor;
    public ParticleSystem[] doorParticles;

    private BoxCollider2D box;
    private SpriteRenderer sprite;
    private float progress;

    void Start() {
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        progress = 0;
    }

    // Update is called once per frame
    void Update() {
        if (DoorOpen) {
            if (progress < 1) {
                if (doorParticles[0].isStopped) {
                    doorParticles[0].Play();
                    doorParticles[1].Play();
                }
                progress += Time.deltaTime / TimetoOpenDoor;
                sprite.color = Color.Lerp(DoorClosedColor, DoorOpenColor, progress);
            }
            else {
                progress = 1;
                sprite.color = DoorOpenColor;
            }
        }
        else {
            if (progress > 0) {
                if (doorParticles[0].isStopped) {
                    doorParticles[0].Play();
                    doorParticles[1].Play();
                }
                progress -= Time.deltaTime / TimetoOpenDoor;
                sprite.color = Color.Lerp(DoorClosedColor, DoorOpenColor, progress);
            }
            else {
                progress = 0;
                sprite.color = DoorClosedColor;
            }
        }

        box.enabled = progress < 0.8;
    }
}
