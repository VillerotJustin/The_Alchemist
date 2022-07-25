using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObject : InteractableObject
{

    [SerializeField] private string dialogFileName;

    protected override void InteractionEvent()
    {
        if(!DialogSystem.instance.inDialog){
            DialogSystem.instance.StartDialog(dialogFileName);
        }
    }
}
