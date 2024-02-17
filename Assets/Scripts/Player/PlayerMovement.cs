using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;

    float finalSpeed;

    //based on input
    float xInput;
    float yInput;

    //Components references
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer graphics;

    PlayerStats stats;

    public static PlayerMovement instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        graphics = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();

        canMove = true;

        instance = this;
    }

    private void Update()
    {
        if(GameStateManager.instance.CurrentGameState == GameState.Gameplay && canMove) UpdateAnim();
    }
    private void FixedUpdate()
    {
        if (GameStateManager.instance.CurrentGameState == GameState.Gameplay && canMove)  Move();
        else rb.velocity = new Vector2( 0, 0);
    }

    void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Change movement during diagonal
        if ((xInput == 1 && yInput == -1) || (xInput == -1 && yInput == 1) || (xInput == 1 && yInput == 1) || (xInput == -1 && yInput == -1)) finalSpeed = stats.movementSpeed * 0.65f;
        else finalSpeed = stats.movementSpeed;

        // Apply movement
        rb.velocity = new Vector2(xInput * finalSpeed, yInput * finalSpeed);

        // Update X axis
        if (rb.velocity.x < -.1f) graphics.flipX = true;
        else if (rb.velocity.x > .1f) graphics.flipX = false;
    }

    void UpdateAnim()
    {
        // Ilde/Move animation
        float VelocityX = Mathf.Abs(rb.velocity.x);
        float VelocityY = Mathf.Abs(rb.velocity.y);
        float characterVelocity;

        if ((xInput == 1 && yInput == -1) || (xInput == -1 && yInput == 1)) characterVelocity = 1;
        else characterVelocity = VelocityX + VelocityY;
        anim.SetFloat("Speed", characterVelocity);
    }
}
