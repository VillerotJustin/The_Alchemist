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

    [SerializeField] private Collider2D spriteCollider;


    public void Init(Collider2D parent){
        canBePickedUp = false;
        startY = transform.position.y;
        side = Random.Range(-1.0f,1.0f);
        height = Random.Range(0.8f,1.2f);
        speed = 2;

        Physics2D.IgnoreCollision(spriteCollider,Player.body.GetComponentInChildren<Collider2D>(),true);
        Physics2D.IgnoreCollision(spriteCollider,parent,true);
    }

    void Update(){
        if(canBePickedUp){
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
        if(canBePickedUp && col.tag=="Player"){
            if(GameManager.player.AddItem(item)){
                PlayerHotBarUI.instance.RefreshHotBar();
                Destroy(gameObject);
            }
        }
    }
}
