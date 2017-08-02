using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour {

    public float MaxProgress;
    public float TimeToFadeIn;

    private Image image;
    private float progress;

    // Use this for initialization
    void Start() {
        image = GetComponent<Image>();
        progress = 0;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(progress);
        if (progress < MaxProgress) {
            progress += Time.unscaledDeltaTime;
            if (progress >= MaxProgress)
                progress = MaxProgress;
            image.color = new Color(image.color.r, image.color.r, image.color.b, Mathf.Lerp(0, MaxProgress, progress));
        }
    }

    public void ResetProgress() {
        progress = 0;
        image.color = new Color(image.color.r, image.color.r, image.color.b, 0);
    }
}
