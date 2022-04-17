using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerBuyButton : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler{
    public Sprite icon;
    public TowerData towerData;

    void Start(){
        GetComponent<Image>().sprite = icon;
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        UiSystem.Instance.PreviewTowerPanelOpen(towerData);
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        UiSystem.Instance.PreviewTowerPanelClose();
    }

    //  Click button
    public void OnPointerClick(PointerEventData pointerEventData){
        UiSystem.Instance.TowerButtonClick(towerData);
    }
}