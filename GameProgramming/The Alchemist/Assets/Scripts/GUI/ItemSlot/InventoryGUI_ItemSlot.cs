using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryGUI_ItemSlot : UI_ItemSlot
{
    

    [SerializeField] private EventTrigger eventTrigger;

    [SerializeField] private float maxTimeToWait = 1;

    private float currentTimeToWait;

    private bool canWaitToShow = false;

    private InventoryGUI inv;

    void Update(){
        if(canWaitToShow){
            if(currentTimeToWait>0){
                currentTimeToWait-=Time.unscaledDeltaTime;
            }else{
                currentTimeToWait = maxTimeToWait;
                canWaitToShow = false;
                InfoUI.instance.ShowInfo(item);
            }
        }
    }

    public void Init(int newSlot,InventoryGUI inventory){
        inv = inventory;
        currentTimeToWait = maxTimeToWait;

        base.Init(newSlot);
    }

    public void OnEnter(BaseEventData eventData){
        if(item == null) return;
        canWaitToShow = true;
        currentTimeToWait = maxTimeToWait;
    }

    public void OnExit(BaseEventData eventData){
        canWaitToShow = false;
        InfoUI.instance.HideInfo();
    }

    public void OnLeftClick(){
        inv.TakeItem(slot);
    }

    public void OnRightClick(){
        inv.PlaceItem(slot);
    }

    public void OnClick(BaseEventData eventData){
        if(Input.GetMouseButton(0)){
            OnLeftClick();
        }else if(Input.GetMouseButton(1)){
            OnRightClick();
        }
    }

}
