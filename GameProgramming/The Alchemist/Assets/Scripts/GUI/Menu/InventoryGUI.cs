using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MenuGUITab
{

    [SerializeField] private PlayerBagGUI bag;


    public override void OnOpen(){
        base.OnOpen();
        bag.OpenBag();
    }

    public override void OnClose(){
        base.OnClose();
        bag.CloseBag();
        bag.DropItem();
    }




}
