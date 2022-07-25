using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected string cursorName;

    
    protected bool canInteract;

    protected bool mouseIn;

    protected bool playerInZone;

    protected void Start(){
        canInteract = true;
        playerInZone = false;
        mouseIn = false;
    }


    protected void RefreshCursor(){
        if(mouseIn && playerInZone){
            GameCursor.ChangeCursor(cursorName);
        }else{
            GameCursor.ChangeCursor("defaultCursor");
        }
    }


    protected void OnTriggerEnter2D(Collider2D col){
        if(!canInteract  || col.tag != "Player") return;
        playerInZone = true;
        RefreshCursor();
    }


    protected void OnTriggerExit2D(Collider2D col){
        if(col.tag != "Player") return;
        playerInZone = false;
        RefreshCursor();
    }

    protected void OnMouseEnter(){
        if(!canInteract) return;
        mouseIn = true;
        RefreshCursor();
    }

    protected void OnMouseExit(){
        mouseIn = false;        
        RefreshCursor();
    }

    protected void Update(){
        if(!playerInZone || !mouseIn || !canInteract) return;

        if(Input.GetMouseButtonDown(0)){
            InteractionEvent();
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

}
