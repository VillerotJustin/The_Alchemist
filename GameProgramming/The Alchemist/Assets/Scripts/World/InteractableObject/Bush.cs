using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : InteractableObject
{

    // TO DO
    // Add system to check season for berries drop
    // Setup global nature spawn system


    [SerializeField] private string itemDropName;

    [SerializeField] private Sprite bushFull;
    [SerializeField] private Sprite bushEmpty;

    [SerializeField] private SpriteRenderer bushRenderer;

    [SerializeField] private Collider2D spriteCollider;


    protected new void Start(){
        base.Start();
        canInteract = true;
    }

    protected new void Update(){
        if(!canWait || !canInteract) return;

        if(Input.GetKeyDown(KeyCode.E)){
            InWorldItem obj = Instantiate(GameManager.prefabInWorldItem,transform.position,new Quaternion()).GetComponent<InWorldItem>();
            obj.Init(spriteCollider,GameManager.instance.GetItemFromName(itemDropName),1);

            DisableObject();
            bushRenderer.sprite = bushEmpty;
        }
    }
}
