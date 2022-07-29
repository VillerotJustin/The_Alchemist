using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBagGUI : MonoBehaviour
{

    [SerializeField] protected GameObject root;

    [SerializeField] protected GameObject prefabItemGUI;

    protected Item itemMoving = null;
    protected int numberItemsMoving = 0;

    [SerializeField] protected InventorySelection_Helper helper;

    public virtual void CloseBag(){
        foreach(Transform child in root.transform){
            Destroy(child.gameObject);
        }
    }


    public virtual void OpenBag(){
        for(int i = 0;i < GameManager.player.bagSize;i++){
            Instantiate(prefabItemGUI,root.transform).GetComponent<InventoryGUI_ItemSlot>().Init(i,this);
        }
    }


    public virtual void DropItem(){
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

    public virtual void PlaceItem(int slot){
        if(itemMoving == null) return;
        bool takeAll = Input.GetKey(KeyCode.LeftShift);

        Player player = GameManager.player;

        if(player.IsItemInSlotSameAs(slot,itemMoving) ||
            player.GetItemFromSlot(slot) == null){

            player.AddItemToSlot(itemMoving,takeAll ? numberItemsMoving : 1,slot);
            numberItemsMoving-= takeAll ? numberItemsMoving : 1;
            if(numberItemsMoving == 0){
                itemMoving = null;
            }
        }

        helper.Refresh(itemMoving,numberItemsMoving);
        RefreshInventory();
        PlayerHotBarUI.instance.RefreshHotBar();
    }

    protected void RefreshInventory(){
        for(int i = 0;i < GameManager.player.bagSize;i++){
            root.transform.GetChild(i).GetComponent<InventoryGUI_ItemSlot>().RefreshSlot();
        }
    }
}
