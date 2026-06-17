using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            stats.AddScore(value);

            SimpleEventBus.Instance.PostNotification(
                GameEventType.PlaySfx,
                this,
                SfxKeys.Collectible
            );

            Destroy(gameObject);
        }
    }
}