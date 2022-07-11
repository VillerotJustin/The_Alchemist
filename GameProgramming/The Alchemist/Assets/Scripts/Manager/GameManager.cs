using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Player player;

    void Awake(){
        if(instance == null){
            instance = this;

            _player = new Player();
            player = _player;

            playerCanMove = true;

            DEBUG_GIVEITEMS();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public bool playerCanMove;

    private Player _player;
    [SerializeField] private Item[] allItems;


    public Item GetItemFromName(string name){
        foreach(Item item in allItems){
            if(item.internalName.Equals(name)){
                return item;
            }
        }
        return null;
    }

    public Item GetItemFromID(int id){
        return allItems[id];
    }

    

    public void DEBUG_GIVEITEMS(){
        player.AddItemToSlot(allItems[0],4,0);
        player.AddItemToSlot(allItems[1],4,5);
    }
}
