using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb; //objectc var of rb
    private BoxCollider2D coll;
    private SpriteRenderer sprite; // to see if facing right ot left
    private Animator anim; // to access state ie run idle

    //dj code
    private bool doubleJump;
    public KeyCode Spacebar;
    public float groundCheckRadius;
    private bool grounded;
    [SerializeField] private LayerMask jumpableGround;  //for isGround checking using ground layer back in  unity
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private int maxJumps = 2;
    private int jumpsLeft;

    private float dirX = 0f; //so can be used down in state method
    [SerializeField] private float moveSpeed = 7f;
    
    //serialize makes priv var accessible in unity inspector
    //not set to public so other scripts dont access it to uphold encapsulation so use serialize field

    private enum MovementState { idle, running, jumping }
    //creating own data type  of movement state made up of whats inside curly braces 
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //getting access to rigid body inside unity
        coll = GetComponent<BoxCollider2D>(); 
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jumpsLeft = maxJumps;
    }


    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //gets input of x as a range altho uses arrows incase joystick fo eg L is -1  is +1 -axis raw so doesnt slide,
                                               //stops when remove hand from arrows
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); //so when jumping doesnt get back immeadiatlty to
                                                                    //ground when moving and speed in var so takes into account joysticks
                                                                    //When user presses the space buXon once
       //spacebar not pressed and its on grnd
       if (grounded && rb.velocity.y <= 0)
        {
            jumpsLeft = maxJumps;
        }

        if (!Input.GetKeyDown(Spacebar) && grounded)                 
        {
            doubleJump = false;
        }
        
        if (Input.GetKeyDown(Spacebar))
        {
            if (grounded || doubleJump)
            {
                Jump();
                doubleJump = !doubleJump;
                
            }
        }

        if (Input.GetKeyDown(Spacebar) && rb.velocity.y>0.5f &&jumpsLeft>0 )
        {  Jump2();
           jumpsLeft--;
        }
      
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f) //move right
        {
            state = MovementState.running; //= to 1 in enum and unity
            sprite.flipX = false; //makes it rights
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true; // makes it look left
        }
        else
        {
            state = MovementState.idle; // =0 in enum and unity
        }
                                 //check for jump after the run bcz priority to jump so if tthats the case will jump
        if (rb.velocity.y > .1f) //0.1 instead of 0 bcz of imprecision ,upward force applied therefore bigger than 0 so jumping
        {
            state = MovementState.jumping; //=2 in enum and unity
        }

        anim.SetInteger("state", (int)state);} //so dont have to keep callling anim.setbool running false w keda
                                               //state is the var in animator used to check ocndition checking which
                                               //enum is there so can make a transition, cast enum to int bcz ecpects int 

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
    }

    void Jump2()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce*1.5f);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableGround);
    }

    //private bool IsGrounded()
    //{
       // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, jumpableGround);
        // return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); //checks if overlapping jumpable ground
        //center of box collider, size of box colllider , angle is 0 , direction is down, distance 0.1 
        //creates a second boxx cast over the capsule collider of same size and at same loc 
   // }
}
