using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State{

    public override void EnterState(GameManager manager){
        manager.Win();
    }
    public override void UpdateState(GameManager manager){
        
    }
}
