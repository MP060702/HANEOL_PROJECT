using UnityEngine;

public class PipeMonsterHead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
            ICreature creature = collision.gameObject.GetComponent<PlayerMovement>();
            if (creature != null)
            {
                creature.Die();
            }
    }
} 