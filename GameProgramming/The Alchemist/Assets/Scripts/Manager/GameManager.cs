using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Player player;

    void Awake(){
        if(instance == null){
            instance = this;
            LoadItems();

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

    private Dictionary<string,Item> allItems;

    [SerializeField] private GameObject _prefabInWorldItem;
    public static GameObject prefabInWorldItem {get{return GameManager.instance._prefabInWorldItem;}}


    [SerializeField] private MovingCharactersManager _movingCharacters;

    public static MovingCharactersManager movingCharacters {get{return GameManager.instance._movingCharacters;}}


    [SerializeField] private RecipeManager _recipeManager;
    public static RecipeManager recipeManager {get{return GameManager.instance._recipeManager;}}

    private string _map;
    public static string map {get{return GameManager.instance._map;}}


    public Item GetItem(string name){
        if(!allItems.ContainsKey(name)) return null;
        return allItems[name];
    }


    public void UpdateTime(){
        dayNightCycle.UpdateTime();
    }


    void LoadItems(){
        allItems = new Dictionary<string, Item>();
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Items/items"));
        string line;
        string[] split;
        Item currentItem = null;
        int howManyThingsSpecified = 0;
        for(int i = 0;i < fileContent.Count;i++){
            line = fileContent[i];
            if(string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

            if(line.StartsWith("[") && line.EndsWith("]")){
                if(howManyThingsSpecified != 6 && currentItem != null){
                    Debug.Log("Error on Item "+currentItem.internalName+". Informations are missing.");
                }else{
                    howManyThingsSpecified = 1;
                    currentItem = new Item();
                    currentItem.internalName = line.Substring(1,line.Length-2);
                }
                continue;
            }

            split = line.Split(" = ");

            if(split.Length != 2){
                Debug.Log("Error on line "+line+". There should be only one = .");
                continue;
            }

            if(split[0].EndsWith(" ")){
                split[0] = split[0].Substring(0,split[0].Length-1);
            }
            if(split[1].EndsWith(" ")){
                split[1] = split[1].Substring(0,split[1].Length-1);
            }

            switch(split[0]){
                case "Name":
                    currentItem.itemName = split[1];
                    howManyThingsSpecified++;
                    break;
                case "Description":
                    currentItem.itemDescription = split[1];
                    howManyThingsSpecified++;
                    break;
                case "Price":
                    currentItem.sellPrice = int.Parse(split[1]);
                    howManyThingsSpecified++;
                    break;
                case "Type":
                    currentItem.itemType = Enum.Parse<Item.Type>(split[1]);
                    howManyThingsSpecified++;
                    break;
                case "Attributes":
                    currentItem.itemAttributes = new List<ItemAttribute>();
                    string[] splittedAttributes = split[1].Split(",");
                    int j = 0;
                    ItemAttribute attr;
                    int result;
                    while(j < splittedAttributes.Length){
                        attr = new ItemAttribute();
                        attr.attributeName = splittedAttributes[j];
                        if(j+1 < splittedAttributes.Length && int.TryParse(splittedAttributes[j+1],out result)){
                            attr.attributeValue = result;
                            j++;
                        }
                        currentItem.itemAttributes.Add(attr);
                        j++;
                    } 
                    howManyThingsSpecified++;
                    break;
            }

            if(howManyThingsSpecified == 6){
                if(!allItems.TryAdd(currentItem.internalName,currentItem)){
                    Debug.Log("Error on Item "+currentItem.internalName+". Item name already exists.");
                }
                currentItem = null;
                howManyThingsSpecified = 0;
            }

        }
    }
    

    public void DEBUG_GIVEITEMS(){
        player.AddItemToSlot(allItems["HEALING_POTION"],4,0);
        player.AddItemToSlot(allItems["WINE"],4,5);
    }
}
