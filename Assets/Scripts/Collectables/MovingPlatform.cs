using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    [Tooltip("If true, the platform stays still until Activate() is called.")]
    public bool startInactive = false;

    private Vector3 target;
    private Vector3 lastPosition;
    private bool isActivated;

    public Vector3 Velocity { get; private set; }

    void Start()
    {
        lastPosition = transform.position;

        if (startInactive)
        {
            enabled = false;
            return;
        }

        BeginMovement();
    }

    public void Activate()
    {
        if (isActivated)
        {
            return;
        }

        isActivated = true;
        enabled = true;
        BeginMovement();
    }

    private void BeginMovement()
    {
        target = pointB;
        lastPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
        }


        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}