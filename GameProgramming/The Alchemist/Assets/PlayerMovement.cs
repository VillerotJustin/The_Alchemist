using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movment;
    // Update is called once per frame
    void Update()
    {
        // input
        movment.x = Input.GetAxisRaw("Horizontal");
        movment.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movment.x);
        animator.SetFloat("Vertical", movment.y);
        animator.SetFloat("speed", movment.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movment * moveSpeed * Time.fixedDeltaTime);
    }
}
