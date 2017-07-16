using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    public LevelController levelController;
    public AudioSource leverSound;

    public bool LeverHit {
        get { return leverHit; }
        set {
            if (!leverHit && value)
                GetComponentInChildren<Animation>().Play("SwitchAnim1");
            else if (leverHit && !value)
                GetComponentInChildren<Animation>().Play("SwitchAnim2");
            leverHit = value;
        }
    }

    private bool leverHit;

    public void HitLever() {
        if (leverHit)
            return;
        Instantiate(leverSound, transform.position, transform.rotation);
        levelController.SpawnLevel();
        LeverHit = true;
    }
}
