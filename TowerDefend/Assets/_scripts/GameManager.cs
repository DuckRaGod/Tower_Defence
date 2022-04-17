using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    //  level data
    public LevelData level;

    //  states
    public State currentState;
    public WinState winState = new();
    public NightState nightState = new();
    public DayState dayState = new();
    public LoseState loseState = new();
    public PauseState pauseState = new();

    public Camera cam;
    public GameData data;

    //  points
    public Transform spawnPoint;
    public Transform[] walkPoint;

    [HideInInspector] public List<int> enemiesAlive = new();
    int enemiesToSpawn;

    public int waves_amount;

    void SetLevel(){
        waves_amount = level.wave.Length;
        data.nightNumber = 0;
        data.towerToBuild = null;
        data.health = 10;
        currentState = dayState;
        SwitchState(currentState);
        cam = Camera.main;
        UiSystem.Instance.UpdateUiGold();
        UiSystem.Instance.UpdateUiHealth();
        data.isPlaying = true;
        data.gold = 100;

        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }

    public static GameManager Instance { get; private set; }

    void Awake(){
        SetLevel();
    }

    void Update(){
        currentState.UpdateState(this);
    }

    public void SwitchState(State _state){
        currentState = _state;
        currentState.EnterState(this);
    }

    public void SpawnWave(){
        enemiesToSpawn = level.wave[data.nightNumber].enemiesAmount;
        for (int i = 0; i < enemiesToSpawn; i++){
            enemiesAlive.Add(i);
        }

        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies(){
        while(!data.isPlaying) yield return null;

        var i = 0;
        while(i < level.wave[data.nightNumber].enemiesAmount){
            if(!data.isPlaying) yield return null;
            yield return new WaitForSeconds(Random.Range(.2f,.9f));
            if(!data.isPlaying) yield return null;
            SpawnEnemy(i);
            i++;
        }
    }

    void SpawnEnemy(int i){
        GameObject[] _prefabs = level.wave[data.nightNumber].prefabs;

        //  picking random enemy to spawn 
        var enemyToSpawn =  _prefabs[Random.Range(0,_prefabs.Length)];

        var _obj = Instantiate(enemyToSpawn,spawnPoint.position,Quaternion.identity);

        var enemy = _obj.GetComponent<Enemy>();
        enemy.manager = this;
        enemy.walkPoints = new Vector3[walkPoint.Length + 1];
        enemy.id = i;
        enemy.walkPoints[0] = spawnPoint.position;
        for (int j = 0; j < walkPoint.Length; j++){
            enemy.walkPoints[j+1] = walkPoint[j].position;
        }
    }

    public void Lose(){
        data.isPlaying = false;
        UiSystem.Instance.LoseCanves.SetActive(true);
    }

    public void Win(){
        data.isPlaying = false;
        UiSystem.Instance.WinCanves.SetActive(true);
    }
}