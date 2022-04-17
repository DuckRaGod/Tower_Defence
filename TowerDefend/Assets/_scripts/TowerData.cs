using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tower")]
public class TowerData : ScriptableObject{
    public string _name;
    public int cost;
    [TextArea]
    public string description;
    public GameObject prefab;
    public int damage;
    public float fireSpeed;

    public GameObject bulletPrefab;
    public LayerMask enemyLayer;
    public GameData gameData;
}
