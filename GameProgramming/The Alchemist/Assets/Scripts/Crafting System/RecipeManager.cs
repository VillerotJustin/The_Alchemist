using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RecipeManager
{
    private List<Recipe> recipes;

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


    public void LoadRecipes(){
        recipes = new List<Recipe>();
        List<string> fileContent = FileManager.ReadTextAsset(Resources.Load<TextAsset>("Items/recipes"));

        string line;
        string[] split;
        Recipe currentRecipe = null;

        for(int i = 0;i < fileContent.Count;i++){
            line = fileContent[i];
            if(line.StartsWith("#")) continue;

            if(line.StartsWith("[RECIPE]") || string.IsNullOrWhiteSpace(line)){
                if(currentRecipe != null){
                    recipes.Add(currentRecipe);
                    currentRecipe = null;
                }

                if(!string.IsNullOrWhiteSpace(line)){
                    currentRecipe = new Recipe();
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
                case "Item1":
                    currentRecipe.item1Name = split[1];
                    break;
                case "Item2":
                    currentRecipe.item2Name = split[1];
                    break;
                case "Result":
                    currentRecipe.resultName = split[1];
                    break;
                case "Item1Count":
                    currentRecipe.item1Count = int.Parse(split[1]);
                    break;
                case "Item2Count":
                    currentRecipe.item2Count = int.Parse(split[1]);
                    break;
                case "ResultCount":
                    currentRecipe.resultCount = int.Parse(split[1]);
                    break;
                case "Machine":
                    currentRecipe.machine =  Enum.Parse<Recipe.Machines>(split[1]);
                    break;
            }
        }
        if(currentRecipe != null){
            recipes.Add(currentRecipe);
        }
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
