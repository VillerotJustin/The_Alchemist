using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotBarUI : MonoBehaviour
{
    public static PlayerHotBarUI instance;

    private int currentSlot = 0;

    private List<UI_ItemSlot> slots;
    [SerializeField] private GameObject slotsRoot;
    [SerializeField] private GameObject slotPrefab;

    void Start()
    {
        slots = new List<UI_ItemSlot>();
        instance = this;
        slotsRoot.SetActive(true);
        CreateHotBar();
    }

    public void CreateHotBar(){
        UI_ItemSlot itemSlot;
        slots.Clear();
        for(int i = 0;i<GameManager.player.maxInHotBar;i++){
            itemSlot = Instantiate(slotPrefab,slotsRoot.transform).GetComponent<UI_ItemSlot>();
            itemSlot.Init(i);
            slots.Add(itemSlot);
        }
        RefreshSelection();
    }

    public void RefreshSelection(){
        slots[currentSlot].GetComponent<Image>().color = Color.gray;
        currentSlot = GameManager.player.currentSlot;
        slots[currentSlot].GetComponent<Image>().color = Color.black;
    }

    public void SetHotBarActive(bool value){
        slotsRoot.SetActive(value);
    }

    public void RefreshHotBar(){
        for(int i = 0;i<GameManager.player.maxInHotBar;i++){
            slots[i].Init(i);
        }
    }


    public int GetCurrentSlot(){
        return currentSlot;
    }

}
