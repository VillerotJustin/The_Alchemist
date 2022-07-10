using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [SerializeField] protected GameObject interactionBuble;

    protected bool canWait;

    protected void Start(){
        canWait = false;
        interactionBuble.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D col){
        canWait = true;
        interactionBuble.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D col){
        canWait = false;
        interactionBuble.SetActive(false);
    }

    protected void Update(){
        if(!canWait) return;

        if(Input.GetKeyDown(KeyCode.E)){
            OnTrigger();
        }
    }

    protected void OnTrigger(){
        print("Object "+name+" triggered");
    }
}
