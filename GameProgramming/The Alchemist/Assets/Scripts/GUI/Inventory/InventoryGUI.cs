using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryRoot;
    [SerializeField] private GameObject prefabItemGUI;

    private bool inventoryOpened;

    private Item itemMoving;
    private int numberItemsMoving;

    [SerializeField] private InventorySelection_Helper helper;

    void Start(){
        inventoryOpened = false;
        inventoryRoot.SetActive(false);
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            inventoryOpened = !inventoryOpened;
            if(inventoryOpened) InitializeInventory();
            else CloseInventory();
        }
    }

    private void InitializeInventory(){
        inventoryRoot.SetActive(true);
        PlayerHotBarUI.instance.SetHotBarActive(false);
        foreach(Transform child in inventoryRoot.transform){
            Destroy(child.gameObject);
        }

        for(int i = 0;i < GameManager.instance.player.bagSize;i++){
            Instantiate(prefabItemGUI,inventoryRoot.transform).GetComponent<InventoryGUI_ItemSlot>().Init(i,this);
        }
    }

    public void CloseInventory(){
        if(itemMoving != null) return;
        inventoryRoot.SetActive(false);
        PlayerHotBarUI.instance.SetHotBarActive(true);
        foreach(Transform child in inventoryRoot.transform){
            Destroy(child.gameObject);
        }
    }

    public void TakeItem(int slot){
        bool takeAll = Input.GetKey(KeyCode.LeftShift);
        Player player = GameManager.instance.player;
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
    }

    public void PlaceItem(int slot){
        if(itemMoving == null) return;

        Player player = GameManager.instance.player;

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
    }

    void RefreshInventory(){
        for(int i = 0;i < GameManager.instance.player.bagSize;i++){
            inventoryRoot.transform.GetChild(i).GetComponent<InventoryGUI_ItemSlot>().Init(i,this);
        }
    }


}
