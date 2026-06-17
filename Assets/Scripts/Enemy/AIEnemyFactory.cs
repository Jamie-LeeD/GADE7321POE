using UnityEngine;

public class AIEnemyFactory : EnemyFactory
{
    private GameObject patrollingPrefab;
    private GameObject stationaryPrefab;
    private GameObject bossPatrollingPrefab;

    public AIEnemyFactory(GameObject patrol, GameObject stationary, GameObject bossPatrol = null)
    {
        patrollingPrefab = patrol;
        stationaryPrefab = stationary;
        bossPatrollingPrefab = bossPatrol;
    }

    // Default (required by abstract class)
    public override Enemy CreateEnemy(Vector3 position)
    {
        return CreateEnemy(EnemyType.Patrolling, position);
    }


    public Enemy CreateEnemy(
        EnemyType type,
        Vector3 position,
        Transform waypointParent = null,
        MyGraph<Transform> bossGraph = null,
        Transform startWaypoint = null)
    {
        GameObject prefab = null;

        switch (type)
        {
            case EnemyType.Patrolling:
                prefab = patrollingPrefab;
                break;

            case EnemyType.Stationary:
                prefab = stationaryPrefab;
                break;

            case EnemyType.BossPatrolling:
                prefab = bossPatrollingPrefab;
                break;
        }

        if (prefab == null)
        {
            Debug.LogError($"AIEnemyFactory: Missing prefab for enemy type {type}.");
            return null;
        }

        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>() ?? obj.GetComponentInChildren<Enemy>();
        if (enemy == null)
        {
            Debug.LogError($"AIEnemyFactory: Prefab '{prefab.name}' has no Enemy component.");
            Object.Destroy(obj);
            return null;
        }

        enemy.Initialize();

        if (type == EnemyType.Patrolling)
        {
            EnemyPatrol patrol = obj.GetComponent<EnemyPatrol>() ?? obj.GetComponentInChildren<EnemyPatrol>();
            if (patrol != null)
            {
                patrol.Initialize(waypointParent);
            }
            else
            {
                Debug.LogError($"AIEnemyFactory: Patrolling prefab '{prefab.name}' has no EnemyPatrol component.");
            }
        }
        else if (type == EnemyType.BossPatrolling)
        {
            BossEnemyPatrol bossPatrol = obj.GetComponent<BossEnemyPatrol>() ?? obj.GetComponentInChildren<BossEnemyPatrol>();
            if (bossPatrol != null && bossGraph != null)
            {
                Transform start = startWaypoint != null ? startWaypoint : waypointParent;
                bossPatrol.Initialize(bossGraph, start);
            }
            else
            {
                Debug.LogError($"AIEnemyFactory: Boss prefab '{prefab.name}' is missing BossEnemyPatrol or graph data.");
            }
        }

        return enemy;
    }
}