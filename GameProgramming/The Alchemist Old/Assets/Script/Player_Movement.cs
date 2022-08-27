using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movspeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;
    void Start()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
        {
            anim.SetFloat("Horizontal_pre", movement.x);
        }
        if (movement.y != 0)
        {
            anim.SetFloat("Horizontal_pre", movement.y);
        }


        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movspeed * Time.fixedDeltaTime);
    }
}
