using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldItem : MonoBehaviour
{
    private float side;
    private float height;

    private float speed;

    private float startY;

    [HideInInspector] public Item item;

    private bool canBePickedUp;

    [HideInInspector] public int numberItems;

    [SerializeField] private Collider2D spriteCollider;

    private bool playerInHitbox;


    public void Init(Collider2D parent,Item itemI,int numberItemsI){
        playerInHitbox = false;
        canBePickedUp = false;
        startY = transform.position.y;
        side = Random.Range(-1.0f,1.0f);
        height = Random.Range(0.8f,1.2f);
        speed = 2;

        item = itemI;
        numberItems = numberItemsI;

        GetComponent<SpriteRenderer>().sprite = item.GetItemSprite();

        Physics2D.IgnoreCollision(spriteCollider,Player.body.GetComponentInChildren<Collider2D>(),true);
        Physics2D.IgnoreCollision(spriteCollider,parent,true);

        InWorldItem[] others = FindObjectsOfType<InWorldItem>();
        foreach(InWorldItem other in others){
            if(other != this){
                Physics2D.IgnoreCollision(spriteCollider,other.spriteCollider,true);
            }
        }
    }

    void Update(){
        if(canBePickedUp){

            if(playerInHitbox){
                if(GameManager.player.AddItem(item,numberItems)){
                    ItemPickupManager.instance.AddItem(item,numberItems);
                    PlayerHotBarUI.instance.RefreshHotBar();
                    Destroy(gameObject);
                    return;
                }
            }

            if(Vector3.Distance(transform.position,Player.body.transform.position) <= 2){
                transform.position = Vector3.MoveTowards(transform.position,Player.body.transform.position,Time.deltaTime * 3);
            }
        }else{
            Fall();
        }

    }

    void Fall(){
        transform.position += new Vector3(side,height,0) * Time.deltaTime * speed;
        height-=Time.deltaTime;

        if(height <= 0){
            if(transform.position.y <= startY){
                canBePickedUp = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col){
        if(col.tag=="Player"){
            playerInHitbox = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            playerInHitbox = false;
        }
    }
}
