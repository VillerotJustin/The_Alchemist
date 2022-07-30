using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBagUI : PlayerBagGUI
{
    [SerializeField] private GameObject realRoot;

    [SerializeField] private Transform allItemsRoot;


    private bool opened;


    void Start(){
        opened = false;
        CloseBag();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F5)){
            Open(!opened);
        }
    }

    public void Open(bool value){
        opened = value;
        realRoot.SetActive(opened);
        if(opened){
            Time.timeScale = 0;
            OpenBag();
        }else{
            Time.timeScale = 1;
            CloseBag();
        }
    }

    public override void OpenBag()
    {
        base.OpenBag();

        foreach(string key in GameManager.instance.allItemsKeys){
            Instantiate(prefabItemGUI,allItemsRoot).GetComponent<InventoryGUI_ItemSlot>().Init(GameManager.instance.GetItem(key),50,this);
        }
        allItemsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            allItemsRoot.GetComponent<RectTransform>().sizeDelta.x,
            allItemsRoot.GetComponent<RectTransform>().sizeDelta.y * GameManager.instance.allItemsKeys.Count/5);
    }

    public override void CloseBag()
    {
        base.CloseBag();
    
        foreach(Transform child in allItemsRoot){
            Destroy(child.gameObject);
        }
    }

}
