using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player
{
    public static GameObject body;
    public InventoryItemContainer[] items;

    public int bagSize;

    public int maxInHotBar;

    public int currentSlot;

    public float speed;

    public int gold;

    public int day;

    public int month;

    public int year;

    public Vector2 startPos;

    public Player(){
        speed = 5;
        maxInHotBar = 8;
        currentSlot = 0;
        bagSize = 24; 
        gold = 0;
        day = 0;
        month = 0;
        year = 1;
        startPos = Vector2.positiveInfinity;
        items = new InventoryItemContainer[bagSize];
    }

    public bool AddItemToSlot(Item item,int nb, int slot){

        if(items[slot] == null){
            items[slot] = new InventoryItemContainer(item,nb);
            return true;
        }

        if(!items[slot].IsItemSameAs(item)){
            return false;
        }

        items[slot].AddToCount(nb);
        return true;
    }

    public void DeleteSlot(int slot){
        items[slot] = null;
    }

    public Item GetItemFromSlot(int slot){
        if(items[slot] == null) return null;
        return items[slot].actualItem;
    }

    public int GetNbItemsInSlot(int slot){
        if(items[slot] == null) return 0;
        return items[slot].itemCount;
    }

    public bool IsItemInSlotSameAs(int slot,Item itemRef){
        if(items[slot] == null){
            return false;
        }
        return items[slot].IsItemSameAs(itemRef);
    }

    public void DecrementSlot(int slot){
        if(items[slot] != null){
            items[slot].itemCount--;
            if(items[slot].itemCount == 0){
                items[slot] = null;
            }
        }
    }

    public bool AddItem(Item item,int number){
        for(int i = 0;i < bagSize;i++){
            if(items[i] != null){
                if(items[i].IsItemSameAs(item)){
                    AddItemToSlot(item,number,i);
                    return true;
                }
            }
        }

        for(int i = 0;i < bagSize;i++){
            if(items[i] == null){
                AddItemToSlot(item,number,i);
                return true;
            }
        }
        return false;
    }

    public bool CanAddItem(Item item){
        for(int i = 0;i < bagSize;i++){
            if(items[i] == null || items[i].IsItemSameAs(item)){
                return true;
            }
        }
        return false;
    }
}
