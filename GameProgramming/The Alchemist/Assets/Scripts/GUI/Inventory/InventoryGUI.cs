using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryRoot;
    [SerializeField] private GameObject prefabItemGUI;

    private bool inventoryOpened;

    void Start(){
        inventoryOpened = false;
        inventoryRoot.SetActive(false);
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            inventoryOpened = !inventoryOpened;
            inventoryRoot.SetActive(inventoryOpened);
            if(inventoryOpened) InitializeInventory();
            else CloseInventory();
        }
    }

    private void InitializeInventory(){
        foreach(Transform child in inventoryRoot.transform){
            Destroy(child.gameObject);
        }

        for(int i = 0;i < GameManager.instance.player.bagSize;i++){
            Instantiate(prefabItemGUI,inventoryRoot.transform).GetComponent<InventoryGUI_ItemSlot>().Init(i);
        }
    }

    public void CloseInventory(){
        foreach(Transform child in inventoryRoot.transform){
            Destroy(child.gameObject);
        }
    }


}
