using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/game")]
public class GameData : ScriptableObject{
    public int gold;
    public int nightNumber;
    public int dayTimeLength;
    public Material daySky;
    public Material nightSky;
    public Material highlight;
    public Material reguler;
    public LayerMask buildableLayer;
    public LayerMask towerLayer;
    public LayerMask not_buildable_layer;
    public TowerData towerToBuild;
    public int health;
    public bool isNight = false;
    public bool isPlaying;
}
