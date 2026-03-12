public class MyStack<T>
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

    private Node top;

    public void Push(T item)
    {
        Node newNode = new Node(item);

        newNode.next = top;
        top = newNode;
    }

    public T Pop()
    {
        if (top == null)
            throw new System.Exception("Stack is empty");

        T item = top.data;
        top = top.next;

        return item;
    }

    public T Peek()
    {
        if (top == null)
            throw new System.Exception("Stack is empty");

        return top.data;
    }

    public bool IsEmpty()
    {
        return top == null;
    }
}