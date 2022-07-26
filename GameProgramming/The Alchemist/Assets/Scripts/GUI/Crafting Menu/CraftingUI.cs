using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{

    public static CraftingUI instance;


    [SerializeField] private GameObject root;

    private bool recipesOpened;

    [SerializeField] private RecipesBookUI book;

    [SerializeField] private CraftingBagUI bag;

    public void Start(){
        recipesOpened = false;
        book.CloseBook();
        instance = this;
        CloseHUD();
        bag.Initialize();
    }

    public void OpenHUD(){
        bag.OpenBag();
        Time.timeScale = 0;
        root.SetActive(true);
    }

    public void CloseHUD(){
        bag.CloseBag();
        bag.DropItem();
        Time.timeScale = 1;
        root.SetActive(false);
    }

    public void SwitchRecipes(){
        recipesOpened = !recipesOpened;
        if(recipesOpened) OpenRecipes();
        else CloseRecipes();
    }

    public void OpenRecipes(){
        book.OpenBook();
    }

    public void CloseRecipes(){
        book.CloseBook();
    }

}
