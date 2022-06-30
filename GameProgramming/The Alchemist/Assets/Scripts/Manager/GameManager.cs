using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
            player = new Player();
            DEBUG_GIVEITEMS();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


    public Player player;
    [SerializeField] private Item[] allItems;



    public void DEBUG_GIVEITEMS(){
        player.AddItemToSlot(allItems[0],4,0);
        player.AddItemToSlot(allItems[0],4,5);
    }
}
