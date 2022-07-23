using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportObject : InteractableObject
{
    [SerializeField] private string teleportToMap;
    [SerializeField] private Vector3 teleportPos;

    protected new void Update(){
        if(!canWait) return;

        if(Input.GetKeyDown(KeyCode.E)){
            // Do the teleport (use a special script and a black loading screen) 
        }
    }
}
