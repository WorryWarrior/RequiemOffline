using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovements : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D body;
    private bool facingRight = true;

    private bool isGrounded;
    public bool IsContactingEnemy { get; set; }

    public Transform topLeft;
    public Transform bottomRight;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpAmount;
    void Start()
    {
        extraJumps = extraJumpAmount;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpAmount;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            body.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded)
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveInput * speed, body.velocity.y);
        FlipHandler();
    }
    
    void FlipHandler()
    {
        void Flip()
        {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        }
        if (!facingRight && moveInput > 0)
        {
            Flip();
        } else if (facingRight && moveInput< 0)
        {
            Flip();
        }
    }
}
