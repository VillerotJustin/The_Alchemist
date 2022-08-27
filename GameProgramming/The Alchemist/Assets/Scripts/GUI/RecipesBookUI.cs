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
        DeleteCurrentRecipes();
    }

    public void ChangeType(int newIndex){
        OpenBook((Recipe.Machines)newIndex);
    }

    public void OpenBook(){
        OpenBook(lastMachine);
    }

    void DeleteCurrentRecipes(){
        foreach(Transform child in recipesRoot.transform){
            Destroy(child.gameObject);
        }
    }


    public void OpenBook(Recipe.Machines machine){
        DeleteCurrentRecipes();
        gameObject.SetActive(true);
        lastMachine = machine;
        List<Recipe> recipes = GameManager.recipeManager.GetAllRecipeInMachine(machine);
        foreach(Recipe recipe in recipes){
            Instantiate(recipePrefab,recipesRoot).GetComponent<RecipeGUI_Recipe>().RefreshRecipe(recipe);
        }
        recipesRoot.sizeDelta = new Vector2(recipesRoot.sizeDelta.x,recipePrefab.GetComponent<RectTransform>().sizeDelta.y * recipes.Count);
    }
}
