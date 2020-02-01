using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool onGround;
    private float x_movement;
    private bool isJumpRepaired = false;
    private bool isSwimRepaired = false;
    private bool isHookRepaired = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public float jumpSpeed = 5f;
    public float max_speed = 4f;
    public float movement_scalar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        manageTools();
        
        // WITH A FORCE 
        x_movement = Input.GetAxis("Horizontal");
        
        if (rb.velocity.magnitude < max_speed)
        {
            Vector2 movement = new Vector2(x_movement, 0);
            rb.AddForce(movement_scalar * movement);
        }

        // WITH VELOCITY
        /*
        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        */
    }

    private void manageTools()
    {
        //JUMP
        if (isJumpRepaired) {
            onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if (onGround && Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //SWIM
        if (isSwimRepaired) {

        }

        //HOOK
        if (isHookRepaired) {

        }
    }
}
