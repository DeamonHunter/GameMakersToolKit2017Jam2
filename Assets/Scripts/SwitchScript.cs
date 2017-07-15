using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    public LevelController levelController;

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
        levelController.SpawnLevel();
        LeverHit = true;
    }
}
