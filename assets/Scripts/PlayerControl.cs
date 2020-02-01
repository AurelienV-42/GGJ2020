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
<<<<<<< Updated upstream
=======
    private double distToGround;
>>>>>>> Stashed changes


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
=======
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }

    private void breakBlock()
    private void manageTools()
    {
        //JUMP
        if (isJumpRepaired) {
            onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if (onGround && Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

<<<<<<< HEAD
        if (Input.GetKey(KeyCode.UpArrow) && onGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
=======
    }
    private void jump()
    {
        onGround = IsGrounded();

        if (Input.GetKey(KeyCode.UpArrow) && onGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
>>>>>>> Stashed changes
=======
        //SWIM
        if (isSwimRepaired) {
>>>>>>> master

        }

        //HOOK
        if (isHookRepaired) {

        }
    }
}
