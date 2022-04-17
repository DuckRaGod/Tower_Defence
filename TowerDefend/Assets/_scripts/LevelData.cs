using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Level")]
public class LevelData : ScriptableObject{
    public Wave[] wave;
}

[System.Serializable]
public class Wave{  // how many waves at level
    public int enemiesAmount;
    public GameObject[] prefabs;    //  variant of enemies to spawn at wave
    //  set deficulty or boss wave
}
