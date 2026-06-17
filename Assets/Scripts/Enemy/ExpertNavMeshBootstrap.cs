using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


[DefaultExecutionOrder(-100)]
public class ExpertNavMeshBootstrap : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    public static bool IsReady { get; private set; }

    void Awake()
    {
        IsReady = false;

        if (navMeshSurface == null)
        {
            navMeshSurface = GetComponent<NavMeshSurface>();
        }

        if (navMeshSurface == null)
        {
            Debug.LogError("ExpertNavMeshBootstrap requires a NavMeshSurface.");
            return;
        }

        navMeshSurface.BuildNavMesh();
        IsReady = true;
    }
}
