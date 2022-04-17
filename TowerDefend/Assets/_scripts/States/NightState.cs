using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightState : State{
    public GameData data;
    GameManager manager;

    void AssignData(GameManager manager){
        if(data == null){
            data = manager.data;
            this.manager = manager;
        }
    }

    public override void EnterState(GameManager manager){
        AssignData(manager);
        SetNight();
    }

    void SetNight(){
        data.isNight = true;
        RenderSettings.skybox = manager.data.nightSky;
        UiSystem.Instance.buildCanves.SetActive(false);
        manager.SpawnWave();
    }

    public override void UpdateState(GameManager manager){
        if (data.isPlaying == false) return;

        //  Lose State
        if(data.health <= 0){
            manager.SwitchState(manager.loseState); 
            return;
        }

        //  Wave win
        if(manager.enemiesAlive.Count > 0) return;
        data.nightNumber++;

        //  Win State
        if(data.nightNumber >= manager.waves_amount){
            manager.SwitchState(manager.winState);
            return;
        }

        manager.SwitchState(manager.dayState);
    }
}
