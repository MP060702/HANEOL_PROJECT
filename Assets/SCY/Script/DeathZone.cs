using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y <= -6f)
        {
            ICreature creature = GetComponent<ICreature>();
            if (creature != null)
            {
                creature.Die();
            }
        }
    }
} 