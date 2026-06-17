using System.Collections.Generic;
public class MyGraph<T>
{

    public class GraphNode
    {
        public T data;
        public MyLinkedList<GraphNode> neighbors;

        public GraphNode(T data)
        {
            this.data = data;
            neighbors = new MyLinkedList<GraphNode>();
        }
    }

    private MyLinkedList<GraphNode> nodes;

    public MyGraph()
    {
        nodes = new MyLinkedList<GraphNode>();
    }

    public GraphNode AddNode(T data)
    {
        GraphNode existing = FindNode(data);
        if (existing != null)
        {
            return existing;
        }

        GraphNode node = new GraphNode(data);
        nodes.Add(node);
        return node;
    }

    public void AddEdge(T from, T to, bool undirected = true)
    {
        GraphNode fromNode = AddNode(from);
        GraphNode toNode = AddNode(to);

        AddNeighborIfMissing(fromNode, toNode);

        if (undirected)
        {
            AddNeighborIfMissing(toNode, fromNode);
        }
    }

    public GraphNode GetNode(T data)
    {
        return FindNode(data);
    }

    public MyLinkedList<GraphNode> GetNeighbors(GraphNode node)
    {
        if (node == null)
        {
            return new MyLinkedList<GraphNode>();
        }

        return node.neighbors;
    }

    public int NodeCount()
    {
        return nodes.Count();
    }

    public MyLinkedList<GraphNode> GetAllNodes()
    {
        return nodes;
    }

    public int GetNeighborCount(GraphNode node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.neighbors.Count();
    }

    private GraphNode FindNode(T data)
    {
        MyLinkedList<GraphNode>.Node current = nodes.GetHead();

        while (current != null)
        {
            if (AreEqual(current.data.data, data))
            {
                return current.data;
            }

            current = current.next;
        }

        return null;
    }

    private void AddNeighborIfMissing(GraphNode fromNode, GraphNode toNode)
    {
        MyLinkedList<GraphNode>.Node current = fromNode.neighbors.GetHead();

        while (current != null)
        {
            if (ReferenceEquals(current.data, toNode))
            {
                return;
            }

            current = current.next;
        }

        fromNode.neighbors.Add(toNode);
    }

    private bool AreEqual(T left, T right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        return EqualityComparer<T>.Default.Equals(left, right);
    }
}
