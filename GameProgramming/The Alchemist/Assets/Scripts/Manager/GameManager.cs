using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Player player;


    [SerializeField] private bool fastBoot;
    private bool firstAsk;

    void Awake(){
        if(instance == null){

            DontDestroyOnLoad(gameObject);

            instance = this;
            Locals.Init();

            firstAsk = true;
            _invertedControls = 1;
            _playerColor = new Color(1,1,1,1);
            
            LoadItems();
            _recipeManager = new RecipeManager();
            _recipeManager.LoadRecipes();

            timeBasedAttributes = new Dictionary<string, ItemAttributeWorker>();
            playerCanMove = true;
            

            if(fastBoot){
                NewGame(true);
                recipeManager.DEBUG_SHOWALLRECIPES();
                DEBUG_GIVEITEMS();
            }

            
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

    public Dictionary<string,Item>.KeyCollection allItemsKeys {get{return allItems.Keys;}}


    private Dictionary<string,ItemAttributeWorker> timeBasedAttributes;

    public Dictionary<string,ItemAttributeWorker>.KeyCollection effectsName {get{return timeBasedAttributes.Keys;}}

    private Color _playerColor;


    public static int currentDay {get{return GameManager.player.day;}}
    public static int currentDayInWeek {get{return GameManager.player.day % 7;}}
    public static int currentMonth {get{return GameManager.player.month;}}

    public static Color playerColor {
        get{return GameManager.instance._playerColor;}
        set{GameManager.instance._playerColor = value;}
        }

    private int _invertedControls;

    public static int invertedControls {
        get{return GameManager.instance._invertedControls;}
        set{GameManager.instance._invertedControls = value;}
    }


    public float lastPlayerOrientationX = 0;

    public float lastPlayerOrientationY = 0;


    private bool _debug;

    public static void ChangeLevel(string newLevel){
        instance._map = newLevel;
        SceneManager.LoadScene(newLevel);
    }

    public static void ChangeLevel(string newLevel,Vector2 coordinates){
        player.startPos = coordinates;
        
        Animator animator = Player.body.GetComponentInChildren<Animator>();
        instance.lastPlayerOrientationX = animator.GetFloat("Horizontal");
        instance.lastPlayerOrientationY = animator.GetFloat("Vertical");

        ChangeLevel(newLevel);
    }

    public void NewGame(bool debug){
        _debug = debug;
        _player = new Player();
        player = _player;
        InitializeNewDay(false);
    }

    public void ToMainMenu(){
        Time.timeScale = 1;
        playerCanMove = true;
        _invertedControls = 1;
        playerColor = new Color(1,1,1,1);
        _movingCharacters.StopAllRoutines();
        StopAllCoroutines();
        PostProcessingManager.ApplyNoEffect();
        timeBasedAttributes.Clear();

        ChangeLevel("TitleScreen");
    }


    public void InitializeNewDay(bool incrementDay){
        if(incrementDay) IncrementDay();

        dayNightCycle.ResetTime();

        movingCharacters.InitializeAllMovingCharacters();
        
        if(!fastBoot || !firstAsk){
            ChangeLevel(_debug ? "TEST_AREA" : "PlayerHome");
        }
        firstAsk = false;


    }

    void IncrementDay(){
        player.day++;
        if(player.day == 28){
            player.day = 0;
            player.month++;
            if(player.month == 4){
                player.month = 0;
                player.year++;
            }
        }
    }

    public Item GetItem(string name){
        if(!allItems.ContainsKey(name)) return null;
        return allItems[name];
    }


    public void UpdateTime(){
        dayNightCycle.UpdateTime();
    }


    public static void ChangeLocalization(string newOne){
        Locals.ChangeLanguage(newOne);
        LocalizedText[] toChange = FindObjectsOfType<LocalizedText>();
        foreach(LocalizedText text in toChange){
            text.ReloadText();
        }
    }


    public static GameObject GetPrefabOfPlaceableObject(string obj){
        return Resources.Load<GameObject>("Items/Placeable/"+obj);
    }

    public void RemoveEffect(string effectName){
        if(EffectExists(effectName)){
            timeBasedAttributes[effectName].StopCoroutine();
            timeBasedAttributes.Remove(effectName);
        }
    }

    public bool EffectExists(string effect){
        return timeBasedAttributes.ContainsKey(effect);
    }

    public float GetTotalTimeOfEffect(string effect){
        return timeBasedAttributes[effect].GetTotalTime();
    }

    public float GetCurrentTimeOfEffect(string effect){
        return timeBasedAttributes[effect].GetCurrentTime();
    }
    
    public void AddEffectsToPlayer(List<ItemAttribute> attributes){
            foreach(ItemAttribute attribute in attributes){

                RemoveEffect(attribute.attributeName);

                timeBasedAttributes.Add(attribute.attributeName,new ItemAttributeWorker(attribute.attributeName,attribute.attributeValue));
                timeBasedAttributes[attribute.attributeName].Init();
            }
    }

    public void SetEffectToEnd(string effect){
        if(EffectExists(effect)){
            timeBasedAttributes[effect].End();
        }
    }


    public void DEBUG_GIVEITEMS(){
        player.AddItemToSlot(allItems["HEALING_POTION"],4,0);
        player.AddItemToSlot(allItems["WINE"],4,5);
    }






    void LoadItems(){
        allItems = new Dictionary<string, Item>();
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Items/items"));
        string line;
        string[] split;
        Item currentItem = null;

        for(int i = 0;i < fileContent.Count;i++){
            line = fileContent[i];
            if(line.StartsWith("#")) continue;

            if((line.StartsWith("[") && line.EndsWith("]") ) || string.IsNullOrWhiteSpace(line)){
                if(currentItem != null){
                    if(!allItems.TryAdd(currentItem.internalName,currentItem)){
                        Debug.Log("Error on Item "+currentItem.internalName+". Item name already exists.");
                    }
                    currentItem = null;
                }

                if(!string.IsNullOrWhiteSpace(line)){
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
                case "Price":
                    currentItem.sellPrice = int.Parse(split[1]);
                    break;
                case "Type":
                    currentItem.itemType = Enum.Parse<Item.Type>(split[1]);
                    break;
                case "Attributes":
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
                    break;
            }
        }
        if(currentItem != null){
            if(!allItems.TryAdd(currentItem.internalName,currentItem)){
                Debug.Log("Error on Item "+currentItem.internalName+". Item name already exists.");
            }
            currentItem = null;
        }


        foreach(Item item in allItems.Values){
            if(Locals.GetLocal(item.internalName+"_Name").Equals("LOCAL_ERROR") || Locals.GetLocal(item.internalName+"_Description").Equals("LOCAL_ERROR"))
                print("Error with item : "+item.internalName);
        }       
    }
}
