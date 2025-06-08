using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float GroundRayLength = 0.1f;
    public Transform GroundCheck;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        CheckGroundRaycast();

        if (!isGrounded && isTouchingWall && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(0f, -1f);
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }

    private void CheckGroundRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundRayLength);
        isGrounded = hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.Die();
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce * 0.5f);
                    }
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isTouchingWall = false;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.9f)
            {
                isTouchingWall = true;
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingWall = false;
    }
}