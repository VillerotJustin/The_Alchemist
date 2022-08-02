using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Animator animator;

    private Vector2 movement;

    void Awake(){
        Player.body = gameObject;
    }

    public void ForceMove(float x,float y){
        movement.x = x;
        movement.y = y;
    }

    void Update()
    {
        if(!GameManager.instance.playerCanMove){
            animator.SetFloat("Speed",0);
            return;
        };
        
        movement.x = Input.GetAxis("Horizontal") * GameManager.invertedControls;
        movement.y = Input.GetAxis("Vertical") * GameManager.invertedControls;

        if(! (movement.x == 0) || !(movement.y == 0)){
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
        }

        animator.SetFloat("Speed",movement.sqrMagnitude);
    }

    void FixedUpdate(){
        if(!GameManager.instance.playerCanMove) return;

        rb.MovePosition(rb.position+movement*GameManager.player.speed*Time.fixedDeltaTime);

        Camera.main.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }
}
