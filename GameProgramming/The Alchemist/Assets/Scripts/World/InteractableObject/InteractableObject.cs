using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [SerializeField] protected GameObject interactionBuble;

    protected bool canWait;
    
    protected bool canInteract;

    protected void Start(){
        canWait = false;
        canInteract = true;
        interactionBuble.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D col){
        if(!canInteract) return;
        canWait = true;
        interactionBuble.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D col){
        canWait = false;
        interactionBuble.SetActive(false);
    }

    protected void Update(){
        if(!canWait || !canInteract) return;

        if(Input.GetKeyDown(KeyCode.E)){
            print("Object "+name+" triggered");
        }
    }

    protected void DisableObject(){
        canInteract = false;
        canWait = false;
        interactionBuble.SetActive(false);
    }

}
