using UnityEngine;
using System.Collections;

public class DayState : State{
    float time;

    GameManager _manager;
    GameData data;
    Camera _cam;
    private Renderer _select;
    Ray ray;

    void SetDay(){
        data.isNight = false;
        _manager.data.health = 10;
        data.gold += 100;
        UiSystem.Instance.UpdateUiGold();
        UiSystem.Instance.UpdateUiHealth();
        RenderSettings.skybox = data.daySky;
        UiSystem.Instance.buildCanves.SetActive(true);
    }

    void SetValue(GameManager manager){
        if(_manager != null) return;
            
        _manager = manager;
        _cam = _manager.cam;
        data = _manager.data;
    }

    public override void EnterState(GameManager manager){
        SetValue(manager);
        SetDay();
    }

    void Outline(Transform hit){
        Transform selection = hit;
        var temp = selection.GetComponent<Renderer>();
        if(_select == temp) return;

        _select = temp;
        _select.material = data.highlight;
    }

    void DeOutline(){
        if(_select == null) return;

        _select.material = data.reguler;
        _select = null;
    }

    void NightTimer(){
        time += Time.deltaTime;
        if(time >= data.dayTimeLength)
        {
            time = 0;
            DeOutline();
            _manager.SwitchState(_manager.nightState);
            return;
        }
    }

    void Build(){
        DeOutline();
                                                                                                                                // cancel operation
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown("escape")){
            data.towerToBuild = null;
            return;
        }

        var layerMaskCombined = data.buildableLayer | data.towerLayer | data.not_buildable_layer;

        if (Physics.Raycast(ray, out RaycastHit hit,1000, layerMaskCombined)){
            if(!hit.transform.CompareTag("Block"))return;
            Collider[] hitColliders = Physics.OverlapSphere(hit.transform.GetChild(0).position, 0.5f , data.towerLayer);
            if(hitColliders.Length > 0) return;
        }else return;

        Outline(hit.transform);
        
        if(data.gold < data.towerToBuild.cost){
            data.towerToBuild = null;
            return;
        }
        if(!Input.GetMouseButtonDown(0)) return;

        UiSystem.Instance.BuildTower(hit.transform.GetChild(0).position);
        data.gold -= data.towerToBuild.cost;
        UiSystem.Instance.UpdateUiGold();
    }

    void TowerMenu(){
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown("escape")){
            UiSystem.Instance.TowerMenuClose();
        }

        DeOutline();
        Tower tower;
        
        if (Physics.Raycast(ray, out RaycastHit hit,1000, data.towerLayer)){
            Outline(hit.transform);
            tower = hit.transform.GetComponent<Tower>();
        }else return;

        if(Input.GetMouseButtonDown(0)){
            UiSystem.Instance.TowerMenuOpen(hit.transform.gameObject);
        }
    }

    public override void UpdateState(GameManager manager){
        if (data.isPlaying == false) return;
        ray = _cam.ScreenPointToRay(Input.mousePosition);

        NightTimer();
        
        if(data.towerToBuild == null) TowerMenu();
        else Build();
    }
}
