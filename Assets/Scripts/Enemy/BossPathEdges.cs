using UnityEngine;

public class BossPathEdges : MonoBehaviour
{
    [System.Serializable]
    public class WaypointEdge
    {
        public Transform waypointA;
        public Transform waypointB;
    }

    public Transform waypointParent;
    public WaypointEdge[] edges;

    void OnDrawGizmosSelected()
    {
        if (edges == null)
        {
            return;
        }

        Gizmos.color = Color.cyan;

        foreach (WaypointEdge edge in edges)
        {
            if (edge.waypointA == null || edge.waypointB == null)
            {
                continue;
            }

            Gizmos.DrawLine(edge.waypointA.position, edge.waypointB.position);
        }
    }
}
