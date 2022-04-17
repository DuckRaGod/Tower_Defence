using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour{
    
    // Todo create pool for each bullet type and not tower

    public Transform bulletTransform;
    readonly List<GameObject> bulletsPool = new();
    public TowerData towerData;
    [HideInInspector] public Transform enemy_transform;
    [HideInInspector] public Enemy enemy;
    float time;

    void Awake(){
        bulletTransform = GameObject.Find("BulletTransform").transform;
    }

    GameObject GetBullet(){
        foreach (var bullet in bulletsPool){
            if(bullet.activeSelf == false){
                return bullet;
            }
        }
        
        var _bull = Instantiate(towerData.bulletPrefab,bulletTransform);
        _bull.SetActive(false);
        bulletsPool.Add(_bull);
        return _bull;
    }

    public virtual void Shoot(Transform shootPoint){
        var _bullet = GetBullet();
        _bullet.SetActive(true);
        _bullet.transform.SetPositionAndRotation(shootPoint.position,shootPoint.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;
    }

    readonly Collider[] _collision = new Collider[1];

    public Transform GetEnemiesNear(){
        var enemies = Physics.OverlapSphereNonAlloc(transform.position,4,_collision , towerData.enemyLayer);
        if(enemies <= 0) return null;
        return _collision[0].transform;
    }

    public void TowerFixedUpdate(){
        if(towerData.gameData.isNight == false || towerData.gameData.isPlaying == false) return; // work at night only

        if(enemy_transform == null){
            enemy_transform = GetEnemiesNear();
        }
        if(enemy_transform == null) return;
        
        if(enemy == null){
            enemy = enemy_transform.parent.GetComponent<Enemy>();
        }
        Rotate();
    }

    void Rotate(){
        var lookPos = enemy_transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }

    public virtual void TimerShoot(Transform shoot_point){
        if(enemy == null) return;

        time += Time.deltaTime;
        if(time >= towerData.fireSpeed){
            enemy.Damage(towerData.damage);
            Shoot(shoot_point);
            time = 0;
        }
    }

    public virtual void Update(){
        if(towerData.gameData.isNight == false || towerData.gameData.isPlaying == false) return; // work at night only
    }
}
