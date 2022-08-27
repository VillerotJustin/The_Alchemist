using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToMap : MonoBehaviour
{

    [SerializeField] private string map;

    [SerializeField] private Vector2 cordinates;

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            GameManager.ChangeLevel(map,cordinates);
        }
    }
}
