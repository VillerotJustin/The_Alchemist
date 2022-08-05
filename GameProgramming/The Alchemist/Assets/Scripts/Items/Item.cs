using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public enum Type {
        HEAD,
        BODY,
        FEET,
        GLOVE,
        TALISMAN,
        USEABLE,
        PLACEABLE,
        TOOL,
        OTHER
    };

    public string internalName;
    //public string itemName;
    //public string itemDescription;
    public int sellPrice;
    public Type itemType;

    public List<ItemAttribute> itemAttributes;

    public Item(){
        itemAttributes = new List<ItemAttribute>();
        sellPrice = 0;
        internalName = "NO_NAME";
        itemType = Type.OTHER;
    }

    public Sprite GetItemSprite(){
        Sprite refSprite = Resources.Load<Sprite>("Items/Sprites/"+internalName);
        if(refSprite == null){
            return Resources.Load<Sprite>("Items/Sprites/PLACEHOLDER");
        }

        return refSprite;
    }
}
