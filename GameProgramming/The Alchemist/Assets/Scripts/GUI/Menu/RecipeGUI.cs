using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGUI : MenuGUITab
{
    [SerializeField] private RecipesBookUI book;

    public override void OnClose(){
        base.OnClose();
        book.CloseBook();
    }


    public override void OnOpen(){
        base.OnOpen();
        book.OpenBook();
    }
}
