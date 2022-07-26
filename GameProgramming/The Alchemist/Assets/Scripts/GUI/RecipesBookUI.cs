using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesBookUI : MonoBehaviour
{
    [SerializeField] private RectTransform recipesRoot;
    [SerializeField] private GameObject recipePrefab;

    private Recipe.Machines lastMachine = Recipe.Machines.ALL;

    public void CloseBook(){
        gameObject.SetActive(false);
        foreach(Transform child in recipesRoot.transform){
            Destroy(child.gameObject);
        }
    }

    public void OpenBook(){
        OpenBook(lastMachine);
    }


    public void OpenBook(Recipe.Machines machine){
        gameObject.SetActive(true);
        lastMachine = machine;
        List<Recipe> recipes = GameManager.recipeManager.GetAllRecipeInMachine(machine);
        foreach(Recipe recipe in recipes){
            Instantiate(recipePrefab,recipesRoot).GetComponent<RecipeGUI_Recipe>().RefreshRecipe(recipe);
        }
        recipesRoot.sizeDelta = new Vector2(recipesRoot.sizeDelta.x,recipePrefab.GetComponent<RectTransform>().sizeDelta.y * recipes.Count);
    }
}
