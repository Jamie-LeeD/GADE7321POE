using UnityEngine;

public class AIEnemyFactory : EnemyFactory
{
    private GameObject patrollingPrefab;
    private GameObject stationaryPrefab;

    public AIEnemyFactory(GameObject patrol, GameObject stationary)
    {
        patrollingPrefab = patrol;
        stationaryPrefab = stationary;
    }

    // Default (required by abstract class)
    public override Enemy CreateEnemy(Vector3 position)
    {
        return CreateEnemy(EnemyType.Patrolling, position);
    }


    public Enemy CreateEnemy(EnemyType type, Vector3 position, Transform waypointParent = null)
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
        }

        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Initialize();

        if (type == EnemyType.Patrolling && waypointParent != null)
        {
            EnemyPatrol patrol = obj.GetComponent<EnemyPatrol>();
            patrol.waypointParent = waypointParent;
        }

        return enemy;
    }
}