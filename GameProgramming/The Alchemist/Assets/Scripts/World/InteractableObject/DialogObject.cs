using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObject : InteractableObject
{

    [SerializeField] private string dialogFileName;
    protected new void Update(){
        if(!canWait) return;

        if(Input.GetKeyDown(KeyCode.E)){
            if(DialogSystem.instance.inDialog){
                DialogSystem.instance.skipDialog = true;
            }else{
                DialogSystem.instance.StartDialog(dialogFileName);
            }

        }
    }
}
