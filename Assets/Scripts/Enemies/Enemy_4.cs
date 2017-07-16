using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : EnemyBase {

    public bool move = false;

    // Update is called once per frame
    protected override void Update() {
        if (Activated) {
            move = true;
        }
    }
     


}
