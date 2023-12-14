using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followGojo : MonoBehaviour
{
    // private Controller player;
    private PlayerMovement player;
    public bool isFacingRight = false;
    public float maxSpeed = 3f;
    public int damage = 3;
    private enum MovementState { idle, running }
    private float dirX = 0f;
    private Animator anim;
    private SpriteRenderer sprite;

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        // Get the player's position
        Vector3 targetPosition = player.transform.position;

        // Set a fixed Y-coordinate for the enemy (adjust this value as needed)
        float fixedY = 0.0f;

        // Create a new position with the fixed Y-coordinate
        Vector3 newPosition = new Vector3(targetPosition.x, fixedY, targetPosition.z);

        // Move the enemy towards the new position
        transform.position = Vector3.MoveTowards(transform.position, newPosition, 2f * Time.deltaTime);

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
            state = MovementState.idle; //=0 in enum and unity
        }
        anim.SetInteger("state", (int)state);
    }
}
