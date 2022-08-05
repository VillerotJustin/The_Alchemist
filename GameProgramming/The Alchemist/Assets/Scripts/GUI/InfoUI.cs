using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public static InfoUI instance;

    [SerializeField] private GameObject infoRoot;

    [SerializeField] private LocalizedText itemName;

    [SerializeField] private Image itemSprite;

    [SerializeField] private LocalizedText itemDescription;

    [SerializeField] private Canvas canvas;


    void Awake()
    {
        instance = this;
        HideInfo();
    }

    void Update(){
        if(infoRoot.activeInHierarchy){
            infoRoot.transform.position = Input.mousePosition;
        }
    }


    public void ShowInfo(Item item){
        if(item == null) return;
        infoRoot.SetActive(true);
        itemName.SetNewKey (item.internalName+"_Name");
        itemDescription.SetNewKey(item.internalName+"_Description");
        itemSprite.sprite = item.GetItemSprite();
    }

    public void HideInfo(){
        infoRoot.SetActive(false);
    }
}
