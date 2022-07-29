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
}
