using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICreature
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float GroundRayLength = 0.1f;
    public Transform GroundCheck;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        if (transform.position.y <= -6f)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        CheckGroundRaycast();

        if (!isGrounded && isTouchingWall && rb.linearVelocity.y > -3f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -6f);
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * MoveSpeed, rb.linearVelocity.y);

        // ÁÂ¿ì ¹ÝÀü
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput);
            transform.localScale = scale;
        }
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

        Debug.DrawRay(GroundCheck.position, Vector2.down * GroundRayLength, isGrounded ? Color.green : Color.red);
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
                    Die();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoin();               
            Destroy(collision.gameObject);              
        }

        if (collision.CompareTag("FinishLine"))
        {   

            SceneManager.LoadScene("SampleScene 1");
        }
    }

    public void Die()
    {
        GameManager.Instance.AddDeath();
        GameManager.Instance.coinCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}