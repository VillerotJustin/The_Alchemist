using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerObject : InteractableObject
{
    private int currentSlot;


    protected override void OnMouseEnter(){
        if(EventSystem.current.IsPointerOverGameObject()) return;
        mouseIn = true;
        RefreshCursor();
    }

    protected override void OnMouseExit(){
        mouseIn = false;        
        RefreshCursor();
    }

    protected override void Update()
    {
        if(!mouseIn || !canInteract) return;

        if(Input.GetMouseButtonDown(0)){
            InteractionEvent();
        }
    }

    protected override void RefreshCursor(){
        if(mouseIn){
            GameCursor.ChangeCursor(cursorName);
        }else{
            GameCursor.ChangeCursor("defaultCursor");
        }
    }


    protected override void InteractionEvent(){
        currentSlot = PlayerHotBarUI.instance.GetCurrentSlot();
        if(GameManager.player.GetItemFromSlot(currentSlot).internalName.Equals("WINE")){
            GameManager.player.DecrementSlot(currentSlot);
            PostProcessingManager.ApplyDrunkFOV();
            PlayerHotBarUI.instance.RefreshHotBar();
        }
    }
}