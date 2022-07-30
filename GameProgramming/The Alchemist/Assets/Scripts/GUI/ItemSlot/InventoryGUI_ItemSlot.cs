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

    public void Init(Item it, int count,PlayerBagGUI inventory){
        inv = inventory;

        base.Init(it,count);
    }

    public virtual void OnLeftClick(){
        inv.TakeItem(this);
    }

    public virtual void OnRightClick(){
        inv.PlaceItem(this);
    }

    public void OnClick(BaseEventData eventData){
        if(Input.GetMouseButton(0)){
            OnLeftClick();
        }else if(Input.GetMouseButton(1)){
            OnRightClick();
        }
    }




    public void AddItem(Item newItem,int nb){
        if(item == null){
            item = newItem;
            itemCount = nb;
        }else if(newItem == item){
            itemCount+=nb; 
        }

        if(slot != -1){
            GameManager.player.AddItemToSlot(newItem,nb,slot);
        }


        RefreshSlot();
    }

    public void ResetSlot(){
        item = null;
        itemCount = 0;
        if(slot != -1){
            GameManager.player.DeleteSlot(slot);
        }
        RefreshSlot();
    }

    public void DecrementSlot(){
        itemCount--;
        if(itemCount == 0){
            item = null;
        }
        if(slot != -1){
            GameManager.player.DecrementSlot(slot);
        }

        RefreshSlot();
    }

    public int GetNbItems(){
        return itemCount;
    }

    public Item GetItem(){return item;}


    public bool IsItemSameAs(Item itemRef){
        if(item == null && itemRef == null) return true;
        if(item == null || itemRef == null) return false;
        return itemRef.internalName.Equals(item.internalName);
    }

}
