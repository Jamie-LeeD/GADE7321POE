using UnityEngine;

public class PlatformActivationTrigger : MonoBehaviour
{
    public MovingPlatform platform;

    public bool triggerOnce = true;

    private bool hasTriggered;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || platform == null)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        platform.Activate();
        hasTriggered = true;

        if (triggerOnce)
        {
            Collider triggerCollider = GetComponent<Collider>();
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
            }
        }
    }
}
