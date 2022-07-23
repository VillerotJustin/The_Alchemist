using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            _map = SceneManager.GetActiveScene().name;
            
            movingCharacters.InitializeAllMovingCharacters();

            recipeManager.DEBUG_SHOWALLRECIPES();
            DEBUG_GIVEITEMS();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public bool playerCanMove;

    private Player _player;

    [SerializeField] private DayNightCycle dayNightCycle;

    public int inGameHour {get{return dayNightCycle.currentHour;}}

    [SerializeField] private Item[] allItems;

    [SerializeField] private GameObject _prefabInWorldItem;
    public static GameObject prefabInWorldItem {get{return GameManager.instance._prefabInWorldItem;}}


    [SerializeField] private MovingCharactersManager _movingCharacters;

    public static MovingCharactersManager movingCharacters {get{return GameManager.instance._movingCharacters;}}


    [SerializeField] private RecipeManager _recipeManager;
    public static RecipeManager recipeManager {get{return GameManager.instance._recipeManager;}}

    private string _map;
    public static string map {get{return GameManager.instance._map;}}


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


    public void UpdateTime(){
        dayNightCycle.UpdateTime();
    }
    

    public void DEBUG_GIVEITEMS(){
        player.AddItemToSlot(allItems[0],4,0);
        player.AddItemToSlot(allItems[1],4,5);
    }
}
