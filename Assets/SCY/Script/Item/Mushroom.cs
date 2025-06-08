using UnityEngine;

public class Mushroom : Item
{
    [SerializeField] LayerMask mask;
    float speed = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isGrounded) return;

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Capsule")
        {
            Destroy(this.gameObject);
        }
    }
}
