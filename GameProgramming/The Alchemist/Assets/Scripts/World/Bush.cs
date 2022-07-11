using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : InteractableObject
{

    // TO DO
    // Add system to check season for berries drop
    // Setup global nature spawn system


    [SerializeField] private string itemDropName;

    [SerializeField] private Sprite bushFull;
    [SerializeField] private Sprite bushEmpty;

    [SerializeField] private SpriteRenderer bushRenderer;


    protected new void Start(){
        base.Start();
        canInteract = true;
    }

    protected new void Update(){
        if(!canWait || !canInteract) return;

        if(Input.GetKeyDown(KeyCode.E)){
            GameManager.player.AddItem(GameManager.instance.GetItemFromName(itemDropName));
            PlayerHotBarUI.instance.RefreshHotBar();

            DisableObject();
            bushRenderer.sprite = bushEmpty;
        }
    }
}
