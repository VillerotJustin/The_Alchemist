using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySelection_Helper : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image sprite;

    void Start(){
        root.SetActive(false);
    }

    void Update()
    {
        if(root.activeInHierarchy){
            root.transform.position = Input.mousePosition;
        }

    }

    public void Refresh(Item item,int nbItems){
        if(item == null){
            root.SetActive(false);
        }else{
            root.SetActive(true);
            sprite.sprite = item.GetItemSprite();
            text.text = "x"+nbItems.ToString();
        }
    }
}
