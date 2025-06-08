using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject item;
    bool getItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getItem = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 7) return;

        if (collision.transform.position.y < transform.position.y && getItem)
        {
            Instantiate(item).transform.position = transform.position + Vector3.up;
            getItem = false;
        }
    }
}
