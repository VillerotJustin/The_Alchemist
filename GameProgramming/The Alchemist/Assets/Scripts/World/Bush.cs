using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : InteractableObject
{
    [SerializeField] private string itemDropName;

    protected new void OnTrigger(){
        GameManager.instance.player.AddItem(GameManager.instance.GetItemFromName(itemDropName));
        PlayerHotBarUI.instance.RefreshHotBar();
    }

    protected new void Update(){
        if(!canWait) return;

        if(Input.GetKeyDown(KeyCode.E)){
            OnTrigger();
        }
    }
}
