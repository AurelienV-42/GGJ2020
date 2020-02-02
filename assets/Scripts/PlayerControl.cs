using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool onGround;
    private bool onBreakable;
    private float x_movement;
    private float groundCheckRadius = 0f;
    private bool isJumpRepaired = true;
    private bool isSwimRepaired = false;
    private bool isHookRepaired = false;

    public Transform topCheck;
    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsBreakable;
    public float jumpForce = 8f;
    public float speed = 4f;
    public Tilemap tileMap; 


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        breakBlock();
        manageTools();

        // WITH POSITION
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;


        // WITH A FORCE 
        /*
        x_movement = Input.GetAxis("Horizontal");
        
        if (rb.velocity.magnitude < max_speed)
        {
            Vector2 movement = new Vector2(x_movement, 0);
            rb.AddForce(movement_scalar * movement);
        }
        */


        // WITH VELOCITY
        /*
        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        */
    }

    private void breakBlock()
    {
        onBreakable = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsBreakable);
        if (onBreakable)
        {
            tileMap.SetTile(tileMap.WorldToCell(groundCheck.position), null);
        }
    }

    private void manageTools()
    {
        //JUMP
        if (isJumpRepaired)
        {
            onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if ((onGround || onBreakable) && Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //SWIM
        if (isSwimRepaired)
        {

        }

        //HOOK
        if (isHookRepaired)
        {

        }
    }
}
