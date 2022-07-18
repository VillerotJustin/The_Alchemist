using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MenuGUITab
{
    [SerializeField] private GameObject prefabItemGUI;


    private Item itemMoving;
    private int numberItemsMoving;

    [SerializeField] private InventorySelection_Helper helper;


    public override void OnOpen(){
        base.OnOpen();

        foreach(Transform child in tabRoot.transform){
            Destroy(child.gameObject);
        }

        for(int i = 0;i < GameManager.player.bagSize;i++){
            Instantiate(prefabItemGUI,tabRoot.transform).GetComponent<InventoryGUI_ItemSlot>().Init(i,this);
        }
    }

    public override void OnClose(){
        base.OnClose();

        if(itemMoving != null) DropItem();
        InfoUI.instance.HideInfo();

        foreach(Transform child in tabRoot.transform){
            Destroy(child.gameObject);
        }
    }

    public void DropItem(){
        if(itemMoving == null || numberItemsMoving == 0) return;

        InWorldItem item = Instantiate(GameManager.prefabInWorldItem,Player.body.transform.position,new Quaternion()).GetComponent<InWorldItem>();
        item.Init(Player.body.GetComponentInChildren<Collider2D>(),itemMoving,numberItemsMoving);

        itemMoving = null;
        numberItemsMoving = 0;
        helper.Refresh(itemMoving,numberItemsMoving);
    }

    public void TakeItem(int slot){
        bool takeAll = Input.GetKey(KeyCode.LeftShift);
        Player player = GameManager.player;
        if(itemMoving == null){
            itemMoving = player.GetItemFromSlot(slot);
            if(itemMoving != null){
                numberItemsMoving = 0;
                int maxLoop = player.GetNbItemsInSlot(slot);
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    player.DecrementSlot(slot);
                    numberItemsMoving++;
                }
            }
        }else{
            if(player.IsItemInSlotSameAs(slot,itemMoving)){
                int maxLoop = player.GetNbItemsInSlot(slot);
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    player.DecrementSlot(slot);
                    numberItemsMoving++;
                }
            }else{
                Item inSlot = player.GetItemFromSlot(slot);
                int nbInSlot = player.GetNbItemsInSlot(slot);

                player.DeleteSlot(slot);
                player.AddItemToSlot(itemMoving,numberItemsMoving,slot);

                itemMoving = inSlot;
                numberItemsMoving = nbInSlot;
            }
        }
        helper.Refresh(itemMoving,numberItemsMoving);
        RefreshInventory();
        PlayerHotBarUI.instance.RefreshHotBar();
    }

    public void PlaceItem(int slot){
        if(itemMoving == null) return;

        Player player = GameManager.player;

        if(player.IsItemInSlotSameAs(slot,itemMoving) ||
            player.GetItemFromSlot(slot) == null){

            player.AddItemToSlot(itemMoving,1,slot);
            numberItemsMoving--;
            if(numberItemsMoving == 0){
                itemMoving = null;
            }
        }

        helper.Refresh(itemMoving,numberItemsMoving);
        RefreshInventory();
        PlayerHotBarUI.instance.RefreshHotBar();
    }

    void RefreshInventory(){
        for(int i = 0;i < GameManager.player.bagSize;i++){
            tabRoot.transform.GetChild(i).GetComponent<InventoryGUI_ItemSlot>().Init(i,this);
        }
    }


}
