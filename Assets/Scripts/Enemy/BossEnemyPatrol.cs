using UnityEngine;
using UnityEngine.AI;

public class BossEnemyPatrol : MonoBehaviour
{
   
    public float arrivalThreshold = 0.5f;

    private MyGraph<Transform> graph;
    private MyGraph<Transform>.GraphNode currentNode;
    private MyGraph<Transform>.GraphNode previousNode;
    private NavMeshAgent agent;


    public void Initialize(MyGraph<Transform> patrolGraph, Transform startWaypoint)
    {
        graph = patrolGraph;
        agent = GetComponent<NavMeshAgent>();

        if (graph == null || startWaypoint == null)
        {
            Debug.LogError("BossEnemyPatrol.Initialize called with null graph or start waypoint.");
            return;
        }

        currentNode = graph.GetNode(startWaypoint);
        previousNode = null;

        if (currentNode == null)
        {
            Debug.LogError("Start waypoint was not found in the boss graph.");
            return;
        }

        if (agent != null)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null)
            {
                agent.speed = enemy.speed;
            }

            agent.SetDestination(currentNode.data.position);
        }
    }

    void Update()
    {
        if (agent == null || currentNode == null)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= arrivalThreshold)
        {
            MoveToNextWaypoint();
        }
    }


    void MoveToNextWaypoint()
    {
        MyGraph<Transform>.GraphNode nextNode = ChooseNextNode();

        if (nextNode == null)
        {
            Debug.LogWarning("BossEnemyPatrol could not find a next waypoint.");
            return;
        }

        previousNode = currentNode;
        currentNode = nextNode;
        agent.SetDestination(currentNode.data.position);
    }

    MyGraph<Transform>.GraphNode ChooseNextNode()
    {
        MyLinkedList<MyGraph<Transform>.GraphNode> neighbors = graph.GetNeighbors(currentNode);
        MyLinkedList<MyGraph<Transform>.GraphNode> candidates = new MyLinkedList<MyGraph<Transform>.GraphNode>();

        MyLinkedList<MyGraph<Transform>.GraphNode>.Node neighborNode = neighbors.GetHead();

        while (neighborNode != null)
        {
            if (previousNode == null || !ReferenceEquals(neighborNode.data, previousNode))
            {
                candidates.Add(neighborNode.data);
            }

            neighborNode = neighborNode.next;
        }

        if (candidates.Count() == 0)
        {
            
            return GetFirstNeighbor(neighbors);
        }

        if (candidates.Count() == 1)
        {
            return candidates.GetHead().data;
        }

  
        int randomIndex = Random.Range(0, candidates.Count());
        return candidates.GetAt(randomIndex);
    }

    private MyGraph<Transform>.GraphNode GetFirstNeighbor(MyLinkedList<MyGraph<Transform>.GraphNode> neighbors)
    {
        MyLinkedList<MyGraph<Transform>.GraphNode>.Node head = neighbors.GetHead();
        return head != null ? head.data : null;
    }
}
