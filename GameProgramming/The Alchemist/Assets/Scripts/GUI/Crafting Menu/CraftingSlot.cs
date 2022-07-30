using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : InventoryGUI_ItemSlot
{
    [SerializeField] private CraftingBagUI crafting;
    public override void OnLeftClick()
    {
        crafting.TakeItemFromCraftingSlot(this);
    }

    public override void OnRightClick()
    {
        crafting.PlaceItemInCraftingSlot(this);
    }


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
