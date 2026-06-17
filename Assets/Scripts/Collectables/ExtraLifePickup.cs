using UnityEngine;

public class ExtraLifePickup : MonoBehaviour
{
    public int lifeAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.AddLife(lifeAmount);
        }

        Destroy(gameObject);
    }
}
