using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : InteractableObject
{
    [SerializeField] private Recipe.Machines machine;

    protected override void InteractionEvent()
    {
        CraftingUI.instance.OpenHUD(machine);
    }

    protected override void InteractionEventRightClick()
    {
        Item item = GameManager.instance.GetItem(machine.ToString());
        if(item != null && GameManager.player.AddItem(item,1)){
            Destroy(gameObject);
            PlayerHotBarUI.instance.RefreshHotBar();
        }
    }
}
