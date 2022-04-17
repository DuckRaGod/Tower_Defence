using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Tower{

    public Transform shootPoint;


    public void FixedUpdate(){ 
        TowerFixedUpdate();
    }

    public override void Update(){
        base.Update();
        TimerShoot(shootPoint);
    }

    public override void TimerShoot(Transform shoot_point){
        base.TimerShoot(shoot_point);
    }
}
