using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : InventoryGUI
{

    public static CraftingUI instance;


    [SerializeField] private GameObject root;

    [SerializeField] private CraftingSlot slot1;
    [SerializeField] private CraftingSlot slot2;

    [SerializeField] private CraftingSlot resultSlot;

    [SerializeField] private Image waterSprite;
    [SerializeField] private Color defaultWaterColor;


    private bool recipesOpened;


    [SerializeField] private RectTransform recipesRoot;
    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private GameObject recipeRootParent;

    public new void Start(){
        recipesOpened = false;
        recipeRootParent.SetActive(false);
        instance = this;
        CloseHUD();
        slot1.RefreshSlot();
        slot2.RefreshSlot();
        resultSlot.RefreshSlot();
        resultSlot.gameObject.SetActive(false);
    }

    public void OpenHUD(){
        OnOpen();
        Time.timeScale = 0;
        root.SetActive(true);
    }

    public void CloseHUD(){
        OnClose();
        Time.timeScale = 1;
        root.SetActive(false);
    }

    public void SwitchRecipes(){
        recipesOpened = !recipesOpened;
        if(recipesOpened) OpenRecipes();
        else CloseRecipes();
    }

    public void OpenRecipes(){
        recipeRootParent.SetActive(true);
        List<Recipe> recipes = GameManager.recipeManager.GetAllRecipeInMachine(Recipe.Machines.ALL);
        foreach(Recipe recipe in recipes){
            Instantiate(recipePrefab,recipesRoot).GetComponent<RecipeGUI_Recipe>().RefreshRecipe(recipe);
        }
        recipesRoot.sizeDelta = new Vector2(recipesRoot.sizeDelta.x,recipePrefab.GetComponent<RectTransform>().sizeDelta.y * recipes.Count);
    }

    public void CloseRecipes(){
        recipeRootParent.SetActive(false);
        foreach(Transform child in recipesRoot.transform){
            Destroy(child.gameObject);
        }
    }


    public void CraftItem(){

        if(resultSlot.GetItem() != null) return;

        Item item1;
        Item item2;
        foreach(Recipe recipe in GameManager.recipeManager.GetAllRecipeInMachine(Recipe.Machines.ALL)){
            item1 = GameManager.instance.GetItemFromName(recipe.item1Name);
            item2 = GameManager.instance.GetItemFromName(recipe.item2Name);
            if((Item1CorrespondsTo(item1,recipe.item1Count) && Item2CorrespondsTo(item2,recipe.item2Count)) || 
            (Item2CorrespondsTo(item1,recipe.item1Count) && Item1CorrespondsTo(item2,recipe.item2Count)) ){
                resultSlot.gameObject.SetActive(true);
                resultSlot.AddItem(GameManager.instance.GetItemFromName(recipe.resultName),recipe.resultCount);
                slot1.ResetSlot();
                slot2.ResetSlot();
                return;
            }
        }
    }


    bool Item1CorrespondsTo(Item item,int number){
        if(!slot1.IsItemSameAs(item)) return false;
        if(item == null) return true;
        return number == slot1.GetNbItems();
    }

    bool Item2CorrespondsTo(Item item,int number){
        if(!slot2.IsItemSameAs(item)) return false;
        if(item == null) return true;
        return number == slot2.GetNbItems();
    }

    public void TakeItemFromCraftingSlot(CraftingSlot slot){
        bool takeAll = Input.GetKey(KeyCode.LeftShift);

        if(itemMoving == null){
            itemMoving = slot.GetItem();
            if(itemMoving != null){
                int maxLoop = slot.GetNbItems();
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    slot.DecrementSlot();
                    numberItemsMoving++;
                }
                if(slot == resultSlot){
                    slot.gameObject.SetActive(false);
                }
            }
        }else{
            if(slot.IsItemSameAs(itemMoving)){
                int maxLoop = slot.GetNbItems();
                for(int i = 0;i< (takeAll ? maxLoop : 1 ) ;i++){
                    slot.DecrementSlot();
                    numberItemsMoving++;
                }
                if(slot == resultSlot){
                    slot.gameObject.SetActive(false);
                }
            }else{
                Item inSlot = slot.GetItem();
                int nbInSlot = slot.GetNbItems();

                slot.ResetSlot();
                slot.AddItem(itemMoving,numberItemsMoving);

                itemMoving = inSlot;
                numberItemsMoving = nbInSlot;
            }
        }
        RefreshWaterColor();
        helper.Refresh(itemMoving,numberItemsMoving);
    }

    public void PlaceItemInCraftingSlot(CraftingSlot slot){
        if(itemMoving == null) return;
        bool takeAll = Input.GetKey(KeyCode.LeftShift);

        Player player = GameManager.player;

        if(slot.IsItemSameAs(itemMoving) ||
            slot.GetItem() == null){


            slot.AddItem(itemMoving,takeAll ? numberItemsMoving : 1);
            numberItemsMoving-= takeAll ? numberItemsMoving : 1;
            if(numberItemsMoving == 0){
                itemMoving = null;
            }
        }

        RefreshWaterColor();
        helper.Refresh(itemMoving,numberItemsMoving);
    }


    void RefreshWaterColor(){
        if(slot1.GetItem() == null && slot2.GetItem() == null){
            waterSprite.color = defaultWaterColor;
        }else{
            Color col1 = slot1.GetAverageColorOfItem();
            Color col2 = slot2.GetAverageColorOfItem();

            if(col1.a == 0){
                waterSprite.color = col2;
            }else if(col2.a == 0){
                waterSprite.color = col1;
            }else{
                waterSprite.color = new Color((col1.r+col2.r)/2,
                (col1.g+col2.g)/2,
                (col1.b+col2.b)/2,
                1);
            }
            

        }
    }
}
