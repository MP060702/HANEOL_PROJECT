using UnityEngine;

public class Enemy : MonoBehaviour, ICreature
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private int moveDirection = 1;
    private SpriteRenderer spriteRenderer;

    public Transform groundCheck;
    public float groundCheckDistance = 0.1f;
    public float wallCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private bool wasTouchingWall = false;
    private bool hasReversedThisFrame = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        hasReversedThisFrame = false;

        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        CheckCliff();
        CheckWall();
    }

    private void CheckCliff()
    {
        Vector2 checkPos = new Vector2(transform.position.x + moveDirection * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, groundCheckDistance, groundLayer);

        if (!hit.collider && !hasReversedThisFrame)
        {
            ReverseDirection();
            hasReversedThisFrame = true;
        }

        Debug.DrawRay(checkPos, Vector2.down * groundCheckDistance, Color.red);
    }

    private void CheckWall()
    {
        Vector2 origin = new Vector2(transform.position.x + moveDirection * 0.5f, transform.position.y);
        Vector2 direction = Vector2.right * moveDirection;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, wallCheckDistance, groundLayer);
        bool isTouchingWall = hit.collider != null;

        if (!wasTouchingWall && isTouchingWall && !hasReversedThisFrame)
        {
            ReverseDirection();
            hasReversedThisFrame = true;
        }

        wasTouchingWall = isTouchingWall;

        Debug.DrawRay(origin, direction * wallCheckDistance, Color.blue);
    }

    private void ReverseDirection()
    {
        moveDirection *= -1;
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = moveDirection == -1;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


}