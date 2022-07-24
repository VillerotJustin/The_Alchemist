using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : InventoryGUI
{
    [SerializeField] private GameObject root;

    [SerializeField] private CraftingSlot slot1;
    [SerializeField] private CraftingSlot slot2;

    [SerializeField] private Image waterSprite;
    [SerializeField] private Color defaultWaterColor;

    private bool craftingMenuOpened;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            craftingMenuOpened = !craftingMenuOpened;
            if(craftingMenuOpened) OpenHUD();
            else CloseHUD();
        }
    }

    public new void Start(){
        craftingMenuOpened = false;
        CloseHUD();
        slot1.RefreshSlot();
        slot2.RefreshSlot();
    }

    public void OpenHUD(){
        OnOpen();
        Time.timeScale = 0;
        root.SetActive(true);
    }

    public void CloseHUD(){
        OnClose();
        Time.timeScale = 1;
        root.SetActive(false);
    }


    public void CraftItem(){

    }

    public void TakeItemFromCraftingSlot(CraftingSlot slot){
        bool takeAll = Input.GetKey(KeyCode.LeftShift);

        if(itemMoving == null){
            itemMoving = slot.GetItem();
            if(itemMoving != null){
                int maxLoop = slot.GetNbItems();
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    slot.DecrementSlot();
                    numberItemsMoving++;
                }
            }
        }else{
            if(slot.IsItemSameAs(itemMoving)){
                int maxLoop = slot.GetNbItems();
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    slot.DecrementSlot();
                    numberItemsMoving++;
                }
            }else{
                Item inSlot = slot.GetItem();
                int nbInSlot = slot.GetNbItems();

                slot.ResetSlot();
                slot.AddItem(itemMoving,numberItemsMoving);

                itemMoving = inSlot;
                numberItemsMoving = nbInSlot;
            }
        }
        RefreshWaterColor();
        helper.Refresh(itemMoving,numberItemsMoving);
    }

    public void PlaceItemInCraftingSlot(CraftingSlot slot){
        if(itemMoving == null) return;
        bool takeAll = Input.GetKey(KeyCode.LeftShift);

        Player player = GameManager.player;

        if(slot.IsItemSameAs(itemMoving) ||
            slot.GetItem() == null){


            slot.AddItem(itemMoving,takeAll ? numberItemsMoving : 1);
            numberItemsMoving-= takeAll ? numberItemsMoving : 1;
            if(numberItemsMoving == 0){
                itemMoving = null;
            }
        }

        RefreshWaterColor();
        helper.Refresh(itemMoving,numberItemsMoving);
    }


    void RefreshWaterColor(){
        if(slot1.GetItem() == null && slot2.GetItem() == null){
            waterSprite.color = defaultWaterColor;
        }else{
            Color col1 = slot1.GetAverageColorOfItem();
            Color col2 = slot2.GetAverageColorOfItem();

            if(col1.a == 0){
                waterSprite.color = col2;
            }else if(col2.a == 0){
                waterSprite.color = col1;
            }else{
                waterSprite.color = new Color((col1.r+col2.r)/2,
                (col1.g+col2.g)/2,
                (col1.b+col2.b)/2,
                1);
            }
            

        }
    }
}
