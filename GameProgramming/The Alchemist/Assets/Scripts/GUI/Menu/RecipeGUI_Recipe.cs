using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeGUI_Recipe : MonoBehaviour
{
    [SerializeField] private Image item1Sprite;
    [SerializeField] private TextMeshProUGUI item1Number;
    [SerializeField] private GameObject item2Root;
    [SerializeField] private Image item2Sprite;
    [SerializeField] private TextMeshProUGUI item2Number;
    [SerializeField] private Image resultSprite;
    [SerializeField] private TextMeshProUGUI resultNumber;

    public void RefreshRecipe(Recipe recipe){
        Item item = GameManager.instance.GetItemFromName(recipe.item1Name);
        item1Sprite.sprite = item.GetItemSprite();
        item1Number.text = "x"+recipe.item1Count;

        item2Root.SetActive(recipe.item2Name != "");
        if(recipe.item2Name != ""){
            item = GameManager.instance.GetItemFromName(recipe.item2Name);
            item2Sprite.sprite = item.GetItemSprite();
            item2Number.text = "x"+recipe.item2Count;
        }

        item = GameManager.instance.GetItemFromName(recipe.resultName);
        resultSprite.sprite = item.GetItemSprite();
        resultNumber.text = "x"+recipe.resultCount;
    }
}
