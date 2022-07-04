using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    private Player player;
    private int startingKeycode = 49;
    void Start()
    {
        player = GameManager.instance.player;
    }


    void Update(){
        for(int i = startingKeycode;i<startingKeycode+player.maxInHotBar;i++){
            if(Input.GetKeyDown((KeyCode)i)){
                player.currentSlot = i-startingKeycode;
                PlayerHotBarUI.instance.RefreshSelection();
                break;
            }
        }
    }
}