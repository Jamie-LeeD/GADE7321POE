using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject patrollingPrefab;
    public GameObject stationaryPrefab;
    public Vector3 patrollingPos;
    public Vector3 stationaryPos;   

    private AIEnemyFactory factory;

    public Transform path1;

    void Start()
    {
        factory = new AIEnemyFactory(patrollingPrefab, stationaryPrefab);

        // Spawn enemies
        factory.CreateEnemy(EnemyType.Patrolling, patrollingPos, path1);
        factory.CreateEnemy(EnemyType.Stationary, stationaryPos);
    }
}