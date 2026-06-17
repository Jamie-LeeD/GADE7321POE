using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform waypointParent;
    public MyLinkedList<Transform> waypoints = new MyLinkedList<Transform>();

    private MyLinkedList<Transform>.Node currentNode;
    private NavMeshAgent agent;

    public void Initialize(Transform parent)
    {
        waypointParent = parent;
        BeginPatrol();
    }

    void Start()
    {
        if (waypoints.Count() == 0)
        {
            BeginPatrol();
        }
    }

    void BeginPatrol()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = new MyLinkedList<Transform>();

        if (waypointParent == null)
        {
            Debug.LogError($"{name}: EnemyPatrol has no waypoint parent assigned.");
            return;
        }

        foreach (Transform wp in waypointParent)
        {
            waypoints.Add(wp);
        }

        currentNode = waypoints.GetHead();

        if (currentNode != null && agent != null)
        {
            agent.SetDestination(currentNode.data.position);
        }
    }

    void Update()
    {
        if (agent == null || currentNode == null) return;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentNode == null) return;

        if (currentNode.next != null)
            currentNode = currentNode.next;
        else
            currentNode = waypoints.GetHead();

        if (currentNode != null)
        {
            agent.SetDestination(currentNode.data.position);
        }
    }
}