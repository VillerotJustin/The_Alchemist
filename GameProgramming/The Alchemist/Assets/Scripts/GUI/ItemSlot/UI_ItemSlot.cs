using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemSlot : MonoBehaviour
{
    protected int slot;
    [SerializeField] protected Image bg;
    [SerializeField] protected Image itemSprite;

    [SerializeField] protected TextMeshProUGUI nbItems;

    protected Item item;

    public void Init(int newSlot){
        slot = newSlot;

        item = GameManager.player.GetItemFromSlot(slot);
        if(item == null){
            itemSprite.color = new Color(0,0,0,0);
            nbItems.text = "";
        }else{
            itemSprite.color = Color.white;
            itemSprite.sprite = item.GetItemSprite();
            nbItems.text = "x"+GameManager.player.GetNbItemsInSlot(slot).ToString();
        }

    }
}
