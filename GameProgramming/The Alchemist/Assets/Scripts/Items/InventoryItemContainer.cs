using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemContainer
{
    public string itemRef;
    public int itemCount;

    public Item actualItem {get{return GameManager.instance.GetItem(itemRef);}}

    public InventoryItemContainer(Item item,int count){
        itemCount = count;
        itemRef = item.internalName;
    }

    public InventoryItemContainer(string item,int count){
        itemCount = count;
        itemRef = item;
    }

    public void AddToCount(int nb){
        itemCount+=nb;
    }

    public bool IsItemSameAs(Item item){
        return item.internalName.Equals(itemRef);
    }
}
