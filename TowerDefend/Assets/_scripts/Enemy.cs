using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
    public int id;
    public int health;
    public int damage;
    public int gold;
    public float durationReachGoal;
    public Vector3[] walkPoints;//  0-----s1-------0------s2-------0------s3--------0------s4----0  | s1+s2+s3+s4 = s | 
    public GameManager manager;

    float speed;
    float distance;
    int point = 1;
    float rand;

    void Awake(){
        rand = Random.Range(0.01f,0.25f);
        durationReachGoal = Random.Range(durationReachGoal-0.5f,durationReachGoal+0.5f);
    }

    void Start(){
        Clac();
        StartCoroutine(GoToTarget());
    }

    void Clac(){
        var segments = walkPoints.Length - 1;

        for (int i = 0; i < segments; i++){
            var start = new Vector2(walkPoints[i].x,walkPoints[i].z);
            var end = new Vector2(walkPoints[i+1].x,walkPoints[i+1].z);

            var segmentDistance = Vector3.Distance(start,end);
            distance += segmentDistance;
        }

        speed = distance / durationReachGoal;
    }

    IEnumerator GoToTarget(){
        var points = walkPoints.Length;
        var t = speed * Time.deltaTime;
        var target = walkPoints[point] + new Vector3(0,0.72f,0);

        while(point < points){
            if(Vector3.Distance(transform.position, target) <= rand){
                point++;
                if(point >= points){
                    ReachedEnd();
                }else{
                    target = walkPoints[point] + new Vector3(0,0.72f,0);
                }
            }
            transform.position = Vector3.MoveTowards(transform.position , target , t);
            yield return null;
        }
    }
    
    public virtual void Die(){
        manager.data.gold += gold;
        manager.enemiesAlive.Remove(id);
        UiSystem.Instance.UpdateUiGold();
        Destroy(gameObject);

    }

    void ReachedEnd(){
        manager.data.health -= damage;
        UiSystem.Instance.UpdateUiHealth();
        manager.enemiesAlive.Remove(id);
        Destroy(gameObject);
    }

    public void Damage(int t){
        health -= t;
        if(health <= 0){
            Die();
        }
    }
}
