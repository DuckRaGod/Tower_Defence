using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : State{

    public override void EnterState(GameManager manager){
        manager.Lose();
    }
    public override void UpdateState(GameManager manager){
        
    }
}
