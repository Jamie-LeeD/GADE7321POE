using UnityEngine;

public static class BossPathGraphBuilder
{

    public static MyGraph<Transform> Build(BossPathEdges edgeSource)
    {
        MyGraph<Transform> graph = new MyGraph<Transform>();

        if (edgeSource == null || edgeSource.edges == null)
        {
            Debug.LogError("BossPathGraphBuilder: No edge source was provided.");
            return graph;
        }

        foreach (BossPathEdges.WaypointEdge edge in edgeSource.edges)
        {
            if (edge.waypointA == null || edge.waypointB == null)
            {
                Debug.LogWarning("BossPathGraphBuilder: Skipping edge with missing waypoint reference.");
                continue;
            }

            graph.AddEdge(edge.waypointA, edge.waypointB, undirected: true);
        }

        if (graph.NodeCount() == 0)
        {
            Debug.LogError("BossPathGraphBuilder: Graph has no valid waypoints.");
        }

        return graph;
    }
}
