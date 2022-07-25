using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : InteractableObject
{


    protected override void InteractionEvent()
    {
        CraftingUI.instance.OpenHUD();
    }
}
