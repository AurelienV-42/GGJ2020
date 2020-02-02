using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool onGround;
    private bool onBreakable;
    private bool onLava;
    private float x_movement;
    private float groundCheckRadius = 0f;
    private int isJumpRepaired = 0;
    private int isSwimRepaired = 0;

    public int isHookRepaired = 0;
    public Transform topCheck;
    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsBreakable;
    public LayerMask whatIsLava;
    public LayerMask whatIsWater;
    public LayerMask whatIsHookable;
    public LayerMask whatIsChestJump;
    public LayerMask whatIsChestHook;
    public float jumpForce = 8f;
    public float speed = 4f;
    public Tilemap breakableTileMap;
    public Animator charac_anim;
    public InGameMenu panel;
    public GameObject jumpEnabled;
    public GameObject hookEnabled;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        manageCollision();
        manageTools();

        // WITH POSITION
        if (Input.GetAxis("Horizontal") < 0)
        {
            charac_anim.SetBool("run_left", true);
            charac_anim.SetBool("run_right", false);
            charac_anim.SetBool("idle", false);
            charac_anim.SetBool("fly", false);

        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            charac_anim.SetBool("run_right", true);
            charac_anim.SetBool("run_left", false);
            charac_anim.SetBool("idle", false);
            charac_anim.SetBool("fly", false);
        }
        else
        {
            charac_anim.SetBool("fly", true);
            charac_anim.SetBool("idle", false);
            charac_anim.SetBool("run_left", false);
            charac_anim.SetBool("run_right", false);
            if (onGround || onBreakable)
            {
                charac_anim.SetBool("idle", true);
                charac_anim.SetBool("fly", false);
            }
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.Escape))
        {
            panel.gameObject.SetActive(true);
            panel.PauseGame();
        }
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

    private void manageCollision()
    {
        onBreakable = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsBreakable);
        if (onBreakable)
            breakableTileMap.SetTile(breakableTileMap.WorldToCell(groundCheck.position), null);
        onLava = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsLava);
        if (onLava)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumpRepaired += (Physics2D.OverlapCircle(rightCheck.position, groundCheckRadius, whatIsChestJump) || Physics2D.OverlapCircle(leftCheck.position, groundCheckRadius, whatIsChestJump) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsChestJump)) ? 1 : 0;
        isHookRepaired += (Physics2D.OverlapCircle(rightCheck.position, groundCheckRadius, whatIsChestHook) || Physics2D.OverlapCircle(leftCheck.position, groundCheckRadius, whatIsChestHook) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsChestHook)) ? 1 : 0;
    }

    private void manageTools()
    {
        //JUMP
        if (isJumpRepaired > 0)
        {
            onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if ((onGround || onBreakable) && Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpEnabled.SetActive(true);
        }

        //SWIM
        if (isSwimRepaired > 0)
        {

        }

        //HOOK
        if (isHookRepaired > 0)
        {
            hookEnabled.SetActive(false);
        }
    }
}
