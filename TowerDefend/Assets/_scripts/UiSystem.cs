using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSystem : MonoBehaviour{
    public GameData data;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI healthText;

    public GameObject buildCanves;
    public GameObject towerPanelPreview; 
    public GameObject tower_menu; 

    public GameObject LoseCanves;
    public GameObject WinCanves;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI cost;

    TowerData tower;
    GameObject tower_object;

    public static UiSystem Instance { get; private set; }

    void Awake(){
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }

    public void UpdateUiGold(){
        goldText.text = data.gold.ToString();
    }

    public void UpdateUiHealth(){
        healthText.text = data.health.ToString();
    }


    public void TowerButtonClick(TowerData towerData){
        data.towerToBuild = towerData;
    }
    
    public void PreviewTowerPanelOpen(TowerData towerData){
        nameText.text = towerData._name;
        description.text = towerData.description;
        damage.text = towerData.damage.ToString() + " Per " + towerData.fireSpeed.ToString() + " seconds.";
        cost.text = towerData.cost.ToString();
        towerPanelPreview.SetActive(true);
    }

    public void PreviewTowerPanelClose(){
        towerPanelPreview.SetActive(false);
    }

    public void TowerMenuOpen(GameObject game_object){
        if(tower_menu.activeSelf == false) {
            tower_menu.SetActive(true);
            buildCanves.SetActive(false);
        }
        tower_object = game_object;
        tower = game_object.GetComponent<Tower>().towerData;
    }

    public void TowerMenuClose(){
        tower_menu.SetActive(false);
        buildCanves.SetActive(true);
    }

    public void BuildTower(Vector3 position){
        Instantiate(data.towerToBuild.prefab,position , Quaternion.identity);
    }

    public void SellTower(){
        GameManager.Instance.data.gold += tower.cost;
        Destroy(tower_object);
        tower_menu.SetActive(false);
        buildCanves.SetActive(true);
    }
}
