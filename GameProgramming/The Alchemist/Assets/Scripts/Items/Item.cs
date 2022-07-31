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
        POTION,
        OINTMENT
    };

    public string internalName;
    public string itemName;
    public string itemDescription;
    public int sellPrice;
    public Type itemType;

    public List<ItemAttribute> itemAttributes;

    public Item(){
        itemAttributes = new List<ItemAttribute>();
        sellPrice = 0;
        internalName = "NO_NAME";
        itemName = "NO_NAME";
        itemDescription = "Cet objet n'a pas de description.";
        itemType = Type.POTION;
    }

    public Sprite GetItemSprite(){
        Sprite refSprite = Resources.Load<Sprite>("Items/Sprites/"+internalName);
        if(refSprite == null){
            Debug.Log("L'image dans Resources/Items/Sprites/"+internalName+" n'existe pas");
            return null;
        }

        return refSprite;
    }
}
