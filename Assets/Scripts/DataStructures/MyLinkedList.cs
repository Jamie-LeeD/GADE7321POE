public class MyLinkedList<T>
{
    public class Node
    {
        public T data;
        public Node next;

        public Node(T data)
        {
            this.data = data;
            next = null;
        }
    }

    private Node head;
    private Node tail;

    public void Add(T item)
    {
        Node newNode = new Node(item);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.next = newNode;
            tail = newNode;
        }
    }

    public T GetAt(int index)
    {
        Node current = head;
        int count = 0;

        while (current != null)
        {
            if (count == index)
                return current.data;

            current = current.next;
            count++;
        }

        throw new System.Exception("Index out of range");
    }

    public int Count()
    {
        int count = 0;
        Node current = head;

        while (current != null)
        {
            count++;
            current = current.next;
        }

        return count;
    }

    public Node GetHead()
    {
        return head;
    }
}