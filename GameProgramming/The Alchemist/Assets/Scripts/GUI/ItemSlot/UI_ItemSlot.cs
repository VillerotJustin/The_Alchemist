using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour
{
    protected int slot;
    [SerializeField] protected Image bg;
    [SerializeField] protected Image itemSprite;

    [SerializeField] protected TextMeshProUGUI nbItems;

    [SerializeField] protected float maxTimeToWait = 1;

    protected float currentTimeToWait;

    protected bool canWaitToShow = false;

    protected Item item;

    protected int itemCount;


    void Update(){
        if(canWaitToShow){
            if(currentTimeToWait>0){
                currentTimeToWait-=Time.unscaledDeltaTime;
            }else{
                currentTimeToWait = maxTimeToWait;
                canWaitToShow = false;
                InfoUI.instance.ShowInfo(item);
            }
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

    public void Init(Item newItem,int newCount){
        slot = -1;
        itemCount = newCount;
        item = newItem;
        currentTimeToWait = maxTimeToWait;
        RefreshSlot();
    }

    public void Init(int newSlot){
        slot = newSlot;
        CopySlot(slot);
        currentTimeToWait = maxTimeToWait;
        RefreshSlot();
    }

    public void CopySlot(int slot){
        item = GameManager.player.GetItemFromSlot(slot);
        itemCount = GameManager.player.GetNbItemsInSlot(slot); 
    }

    public virtual void RefreshSlot(){
        if(slot != -1) CopySlot(slot);
        if(item == null){
            itemSprite.color = new Color(0,0,0,0);
            nbItems.text = "";
        }else{
            itemSprite.color = Color.white;
            itemSprite.sprite = item.GetItemSprite();
            nbItems.text = "x"+itemCount.ToString();
        }
    }


}
