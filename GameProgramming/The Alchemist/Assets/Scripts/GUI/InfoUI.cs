using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public static InfoUI instance;

    [SerializeField] private GameObject infoRoot;

    [SerializeField] private TextMeshProUGUI itemName;

    [SerializeField] private Image itemSprite;

    [SerializeField] private TextMeshProUGUI itemDescription;

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
        infoRoot.SetActive(true);
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
        itemSprite.sprite = item.GetItemSprite();
    }

    public void HideInfo(){
        infoRoot.SetActive(false);
    }
}
