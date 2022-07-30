using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeGUI_Recipe : MonoBehaviour
{

    [SerializeField] private UI_ItemSlot item1Slot;

    [SerializeField] private UI_ItemSlot item2Slot;

    [SerializeField] private UI_ItemSlot resultSlot;

    [SerializeField] private GameObject item2Root;


    public void RefreshRecipe(Recipe recipe){
        item1Slot.Init(GameManager.instance.GetItem(recipe.item1Name),recipe.item1Count);

        item2Root.SetActive(recipe.item2Name != "");
        if(recipe.item2Name != ""){
            item2Slot.Init(GameManager.instance.GetItem(recipe.item2Name),recipe.item2Count);
        }

        resultSlot.Init(GameManager.instance.GetItem(recipe.resultName),recipe.resultCount);
    }
}
