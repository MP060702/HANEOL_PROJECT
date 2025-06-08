using UnityEngine;

public class Enemy : MonoBehaviour, ICreature
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private int moveDirection = 1; // 1: 오른쪽, -1: 왼쪽
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Mathf.Abs(contact.normal.x) > 0.5f)
                {
                    moveDirection = contact.normal.x > 0 ? 1 : -1;
                    // flipX로 스프라이트 반전
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.flipX = moveDirection == -1;
                    }
                    break;
                }
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}