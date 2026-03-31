using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit hazard!");

            // Fire event instead of directly changing lives
            SimpleEventBus.Instance.PostNotification(
                GameEventType.PlayerDied,
                this
            );
        }
    }
}
