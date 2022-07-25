using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportObject : InteractableObject
{
    [SerializeField] private string teleportToMap;
    [SerializeField] private Vector3 teleportPos;



    protected override void InteractionEvent()
    {
        print("PIIIOIOOIII");
    }
}
