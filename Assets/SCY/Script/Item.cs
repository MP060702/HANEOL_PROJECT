using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float GroundRayLength = 2f;
    public bool isGrounded;
    [SerializeField] LayerMask layerM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public virtual void Update()
    {
        if (!isGrounded) transform.position += Vector3.down * 2.5f * Time.deltaTime;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        CheckGroundRaycast();

    }

    private void CheckGroundRaycast()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + Vector3.down * 0.5f,
            new Vector2(0.9f, 0.1f), 0, Vector2.down, 0.1f, layerM);
        isGrounded = hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + Vector3.down * 0.5f, new Vector2(1, 0.1f));
    }

    
}
