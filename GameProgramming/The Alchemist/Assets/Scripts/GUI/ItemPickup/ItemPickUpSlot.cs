using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickUpSlot : MonoBehaviour
{
    private Item itemInSlot;
    private int numberInSlot;

    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI textName;
    

    public void Reload(Item item,int number){
        itemInSlot = item;
        numberInSlot = number;
        sprite.sprite = item.GetItemSprite();
        textName.text = item.itemName;
        textNumber.text = "x"+number.ToString();
    }

    public void AddToItem(int additive){
        numberInSlot+=additive;
        textNumber.text = "x"+numberInSlot.ToString();
    }

    public bool IsItemSameHas(Item item){
        return itemInSlot.internalName.Equals(item.internalName);
    }
}
