using UnityEngine;

public class ExtraLifePickup : MonoBehaviour
{
    public int lifeAmount = 1;

    [Header("Floating Visual")]
    public Transform visualRoot;
    [Min(0f)] public float floatHeight = 0.15f;
    [Min(0f)] public float floatSpeed = 1f;

    private Vector3 visualStartLocalPosition;
    private float phaseOffset;

    void Awake()
    {
        if (visualRoot == null && transform.childCount > 0)
        {
            visualRoot = transform.GetChild(0);
        }

        if (visualRoot == null)
        {
            enabled = false;
            return;
        }

        visualStartLocalPosition = visualRoot.localPosition;
        phaseOffset = transform.position.x + transform.position.z;
    }

    void Update()
    {
        float yOffset = Mathf.Sin((Time.time * floatSpeed) + phaseOffset) * floatHeight;
        visualRoot.localPosition = visualStartLocalPosition + Vector3.up * yOffset;
    }

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
