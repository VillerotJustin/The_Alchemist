using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player
{
    public InventoryItemContainer[] items;

    public int bagSize;

    public int currentSlot;

    public Player(){
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

    public Item GetItemFromSlot(int slot){
        if(items[slot] == null) return null;
        return items[slot].itemRef;
    }

    public int GetNbItemsInSlot(int slot){
        if(items[slot] == null) return 0;
        return items[slot].itemCount;
    }
}
