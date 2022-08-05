using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected string cursorName;

    
    protected bool canInteract;

    protected bool mouseIn;

    protected bool playerInZone;

    protected virtual void Start(){
        canInteract = true;
        playerInZone = false;
        mouseIn = false;
    }


    protected virtual void RefreshCursor(){
        if(mouseIn && playerInZone){
            GameCursor.ChangeCursor(cursorName);
        }else{
            GameCursor.ChangeCursor("defaultCursor");
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D col){
        if(!canInteract  || col.tag != "Player" ) return;
        playerInZone = true;
        RefreshCursor();
    }


    protected virtual void OnTriggerExit2D(Collider2D col){
        if(col.tag != "Player") return;
        playerInZone = false;
        RefreshCursor();
    }

    protected virtual void OnMouseEnter(){
        if(!canInteract || EventSystem.current.IsPointerOverGameObject()) return;
        mouseIn = true;
        RefreshCursor();
    }

    protected virtual void OnMouseExit(){
        mouseIn = false;        
        RefreshCursor();
    }

    protected virtual void Update(){
        if(!playerInZone || !mouseIn || !canInteract) return;

        if(Input.GetMouseButtonDown(0)){
            InteractionEvent();
        }else if(Input.GetMouseButtonDown(1)){
            InteractionEventRightClick();
        }
    }

    protected void DisableObject(){
        canInteract = false;
        mouseIn = false;
        playerInZone = false;
        GameCursor.ChangeCursor("defaultCursor");
    }


    protected virtual void InteractionEvent(){
        print("Object "+name+" triggered");
    }


    protected virtual void InteractionEventRightClick(){
    }

}
