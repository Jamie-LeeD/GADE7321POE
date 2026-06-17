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
        if (patrollingPrefab == null || stationaryPrefab == null)
        {
            Debug.LogError($"{name}: EnemySpawner is missing enemy prefab references.");
            return;
        }

        factory = new AIEnemyFactory(patrollingPrefab, stationaryPrefab);

        factory.CreateEnemy(EnemyType.Patrolling, patrollingPos, path1);
        factory.CreateEnemy(EnemyType.Stationary, stationaryPos);
    }
}