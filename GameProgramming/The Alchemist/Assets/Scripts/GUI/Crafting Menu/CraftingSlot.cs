using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : InventoryGUI_ItemSlot
{
    [SerializeField] private CraftingUI crafting;
    private int nbItemsInSlot = 0;
    public override void OnLeftClick()
    {
        crafting.TakeItemFromCraftingSlot(this);
    }

    public override void OnRightClick()
    {
        crafting.PlaceItemInCraftingSlot(this);
    }



    public void AddItem(Item newItem,int nb){
        if(item == null){
            item = newItem;
            nbItemsInSlot = nb;
        }else if(newItem == item){
            nbItemsInSlot+=nb; 
        }

        RefreshSlot();
    }

    public void ResetSlot(){
        item = null;
        nbItemsInSlot = 0;
        RefreshSlot();
    }

    public void DecrementSlot(){
        nbItemsInSlot--;
        if(nbItemsInSlot == 0){
            item = null;
        }
        RefreshSlot();
    }

    public int GetNbItems(){
        return nbItemsInSlot;
    }

    public void RefreshSlot(){
        if(item == null){
            itemSprite.color = new Color(0,0,0,0);
            nbItems.text = "";
        }else{
            itemSprite.color = Color.white;
            itemSprite.sprite = item.GetItemSprite();
            nbItems.text = "x"+nbItemsInSlot.ToString();
        }
    }


    public bool IsItemSameAs(Item itemRef){
        if(item == null && itemRef == null) return true;
        if(item == null || itemRef == null) return false;
        return itemRef.internalName.Equals(item.internalName);
    }

    public Item GetItem(){return item;}


    public Color GetAverageColorOfItem(){
        if(item == null) return new Color(0,0,0,0);

        Color[] pixels = item.GetItemSprite().texture.GetPixels();
        int total = 0;
        float r = 0;
        float g = 0;
        float b = 0;

        foreach(Color col in pixels){
            if(col.a == 1f){
                total++;
                r += col.r;
                g += col.g;
                b += col.b;
            }
        }

        return new Color(r/total,g/total,b/total,1);
    }
}
