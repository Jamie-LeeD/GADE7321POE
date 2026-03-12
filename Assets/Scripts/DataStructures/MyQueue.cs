public class MyQueue<T>
{
    private class Node
    {
        public T data;
        public Node next;

        public Node(T data)
        {
            this.data = data;
            next = null;
        }
    }

    private Node front;
    private Node rear;

    public void Enqueue(T item)
    {
        Node newNode = new Node(item);

        if (rear == null)
        {
            front = rear = newNode;
            return;
        }

        rear.next = newNode;
        rear = newNode;
    }

    public T Dequeue()
    {
        if (front == null)
            throw new System.Exception("Queue is empty");

        T item = front.data;

        front = front.next;

        if (front == null)
            rear = null;

        return item;
    }

    public T Peek()
    {
        if (front == null)
            throw new System.Exception("Queue is empty");

        return front.data;
    }

    public bool IsEmpty()
    {
        return front == null;
    }
}
