using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemContainer
{
    public Item itemRef;
    public int itemCount;

    public InventoryItemContainer(Item item,int count){
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
