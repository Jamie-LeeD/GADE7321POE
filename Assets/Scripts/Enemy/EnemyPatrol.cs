using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform waypointParent;
    public MyLinkedList<Transform> waypoints = new MyLinkedList<Transform>();

    private MyLinkedList<Transform>.Node currentNode;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform wp in waypointParent)
        {
            waypoints.Add(wp);
        }

        currentNode = waypoints.GetHead();

        if (currentNode != null)
            agent.SetDestination(currentNode.data.position);
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentNode.next != null)
            currentNode = currentNode.next;
        else
            currentNode = waypoints.GetHead();

        agent.SetDestination(currentNode.data.position);
    }
}