using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject patrollingPrefab;
    public GameObject stationaryPrefab;

    private AIEnemyFactory factory;

    void Start()
    {
        factory = new AIEnemyFactory(patrollingPrefab, stationaryPrefab);

        // Spawn enemies
        factory.CreateEnemy(EnemyType.Patrolling, new Vector3(0, 0, 10));
        factory.CreateEnemy(EnemyType.Stationary, new Vector3(5, 0, 15));
    }
}