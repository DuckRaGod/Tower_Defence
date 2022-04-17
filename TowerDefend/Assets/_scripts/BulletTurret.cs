using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour{
    public float timeDeactivate;
    float time;

    void Update(){
        time += Time.deltaTime;
        if(timeDeactivate % time == 0 || timeDeactivate % time == timeDeactivate){
            time = 0;
            gameObject.SetActive(false);
        }
    }
}
