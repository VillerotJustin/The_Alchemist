using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingGfx : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Image colorable; 
    [SerializeField] private Color defaultColor;

    public void ShowMachine(){
        root.SetActive(true);
    }

    public void HideMachine(){
        root.SetActive(false);
    }


    public void SetWaterColor(Color col){
        colorable.color = col;
    }

    public void ResetWaterColor(){
        colorable.color = defaultColor;
    }
}
