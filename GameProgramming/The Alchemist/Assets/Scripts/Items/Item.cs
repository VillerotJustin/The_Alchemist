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

    public ItemAttribute[] itemAttributes;

    public Sprite GetItemSprite(){
        Sprite refSprite = Resources.Load<Sprite>("Items/"+internalName);
        if(refSprite == null){
            Debug.Log("L'image dans Resources/Items/"+internalName+" n'existe pas");
            return null;
        }

        return Resources.Load<Sprite>("Items/"+internalName);
    }
}
