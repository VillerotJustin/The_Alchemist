using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryGUI_ItemSlot : UI_ItemSlot
{
    

    [SerializeField] protected EventTrigger eventTrigger;


    protected PlayerBagGUI inv;


    public void Init(int newSlot,PlayerBagGUI inventory){
        inv = inventory;

        base.Init(newSlot);
    }

    public virtual void OnLeftClick(){
        inv.TakeItem(slot);
    }

    public virtual void OnRightClick(){
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
