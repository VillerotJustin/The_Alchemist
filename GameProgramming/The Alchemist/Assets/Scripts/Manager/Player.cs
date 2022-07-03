using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player
{
    public InventoryItemContainer[] items;

    public int bagSize;

    public int maxInHotBar;

    public int currentSlot;

    public Player(){
        maxInHotBar = 8;
        currentSlot = 0;
        bagSize = 24; 
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
        return items[slot].itemRef;
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
}
