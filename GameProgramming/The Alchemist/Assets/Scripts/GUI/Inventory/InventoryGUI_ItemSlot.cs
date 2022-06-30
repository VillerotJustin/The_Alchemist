using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryGUI_ItemSlot : MonoBehaviour
{
    private int slot;
    [SerializeField] private Image bg;
    [SerializeField] private Image itemSprite;

    [SerializeField] private TextMeshProUGUI nbItems;

    private Item item;

    [SerializeField] private EventTrigger eventTrigger;

    [SerializeField] private float maxTimeToWait = 1;

    private float currentTimeToWait;

    private bool canWaitToShow = false;

    void Update(){
        if(canWaitToShow){
            if(currentTimeToWait>0){
                currentTimeToWait-=Time.deltaTime;
            }else{
                currentTimeToWait = maxTimeToWait;
                canWaitToShow = false;
                InfoUI.instance.ShowInfo(item);
            }
        }
    }

    public void Init(int newSlot){
        currentTimeToWait = maxTimeToWait;
        slot = newSlot;

        item = GameManager.instance.player.GetItemFromSlot(slot);
        if(item == null){
            itemSprite.color = new Color(0,0,0,0);
            nbItems.text = "";
        }else{
            itemSprite.sprite = item.GetItemSprite();
            nbItems.text = "x"+GameManager.instance.player.GetNbItemsInSlot(slot).ToString();
        }

    }

    public void OnEnter(BaseEventData eventData){
        if(item == null) return;
        canWaitToShow = true;
        currentTimeToWait = maxTimeToWait;
    }

    public void OnExit(BaseEventData eventData){
        canWaitToShow = false;
        InfoUI.instance.HideInfo();
    }

}
