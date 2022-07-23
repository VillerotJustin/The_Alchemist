using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGUI : MenuGUITab
{
    [SerializeField] private RectTransform recipesRoot;
    [SerializeField] private GameObject recipePrefab;

    public override void OnClose(){
        base.OnClose();
        foreach(Transform child in recipesRoot.transform){
            Destroy(child.gameObject);
        }
    }


    public override void OnOpen(){
        base.OnOpen();
        List<Recipe> recipes = GameManager.recipeManager.GetAllRecipeInMachine(Recipe.Machines.ALL);
        foreach(Recipe recipe in recipes){
            Instantiate(recipePrefab,recipesRoot).GetComponent<RecipeGUI_Recipe>().RefreshRecipe(recipe);
        }
        recipesRoot.sizeDelta = new Vector2(recipesRoot.sizeDelta.x,recipePrefab.GetComponent<RectTransform>().sizeDelta.y * recipes.Count);
    }
}
