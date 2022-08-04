using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private Vector2 mousePos;
    
    public static PlayerItemPlacer instance;

    void Awake(){
        instance = this;
        enabled = false;
    }


    void Update(){
        if(Input.GetMouseButton(0)){
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(Mathf.Round(mousePos.x),Mathf.Round(mousePos.y));

            RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.zero,Mathf.Infinity,mask);
            if(hit.collider == null){
                int slot = GameManager.player.currentSlot;
                Instantiate(GameManager.GetPrefabOfPlaceableObject(GameManager.player.GetItemFromSlot(slot).internalName),transform.position, new Quaternion());
                GameManager.player.DecrementSlot(slot);
                PlayerHotBarUI.instance.RefreshHotBar();
                if(GameManager.player.GetItemFromSlot(slot) == null) enabled = false;
            }
        }
    }

}
