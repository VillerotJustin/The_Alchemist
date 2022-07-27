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
        POTION
    };

    public string internalName;
    public string itemName;
    public string itemDescription;
    public int sellPrice;
    public Type itemType;

    public List<ItemAttribute> itemAttributes;

    public Sprite GetItemSprite(){
        Sprite refSprite = Resources.Load<Sprite>("Items/Sprites/"+internalName);
        if(refSprite == null){
            Debug.Log("L'image dans Resources/Items/Sprites/"+internalName+" n'existe pas");
            return null;
        }

        return refSprite;
    }
}
