using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Animator animator;

    Vector2 movement;

    void Update()
    {
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if(! (movement.x == 0) || !(movement.y == 0)){
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
        }

        animator.SetFloat("Speed",movement.sqrMagnitude);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);

        Camera.main.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }
}
