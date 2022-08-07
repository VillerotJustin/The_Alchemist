using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapSeasonModifier : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < transform.childCount;i++){
            transform.GetChild(i).gameObject.SetActive(i == GameManager.currentMonth);
        }
    }
}
