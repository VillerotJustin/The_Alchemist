using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeManager
{
    [SerializeField] private List<Recipe> recipes;

    public void ResetRecipes(){
        foreach(Recipe recipe in recipes){
            recipe.discovered = false;
        }
    }


    public void DEBUG_SHOWALLRECIPES(){
        foreach(Recipe recipe in recipes){
            recipe.discovered = true;
        } 
    }

    public Recipe GetRecipe(int index){
        return recipes[index];
    }

    public void DoRecipe(int index){
        recipes[index].discovered = true;
    }


    public List<Recipe> GetAllRecipeInMachine(Recipe.Machines machine){     
        
        List<Recipe> res = new List<Recipe>();
        for(int i = 0;i < recipes.Count;i++){
            if(recipes[i].discovered &&
             (machine == Recipe.Machines.ALL || recipes[i].machine == machine)){
                res.Add(recipes[i]);
            }
        }
        return res;
    }

}
