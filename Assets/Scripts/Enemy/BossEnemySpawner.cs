using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(10)]
public class BossEnemySpawner : MonoBehaviour
{
    public GameObject bossPatrollingPrefab;
    public BossPathEdges pathEdges;
    public Transform startWaypoint;
    public Vector3 spawnPosition;

    void Start()
    {
        if (!ExpertNavMeshBootstrap.IsReady)
        {
            Debug.LogWarning("NavMesh bootstrap was not ready. Boss spawn may fail without walkable NavMesh.");
        }
        if (bossPatrollingPrefab == null)
        {
            Debug.LogError("BossEnemySpawner is missing the boss prefab reference.");
            return;
        }

        if (pathEdges == null)
        {
            Debug.LogError("BossEnemySpawner is missing BossPathEdges.");
            return;
        }

        MyGraph<Transform> graph = BossPathGraphBuilder.Build(pathEdges);

        if (graph.NodeCount() == 0)
        {
            return;
        }

        AIEnemyFactory factory = new AIEnemyFactory(null, null, bossPatrollingPrefab);
        Vector3 spawnPoint = GetValidSpawnPosition(spawnPosition);

        factory.CreateEnemy(
            EnemyType.BossPatrolling,
            spawnPoint,
            waypointParent: null,
            bossGraph: graph,
            startWaypoint: startWaypoint
        );
    }

    private Vector3 GetValidSpawnPosition(Vector3 desiredPosition)
    {
        if (NavMesh.SamplePosition(desiredPosition, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        Debug.LogWarning("Could not find NavMesh near spawn position. Using raw position.");
        return desiredPosition;
    }
}
