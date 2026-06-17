using System.Collections.Generic;

public class MyHashMap<TKey, TValue>
{
    private class HashEntry
    {
        public TKey key;
        public TValue value;

        public HashEntry(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    private MyLinkedList<HashEntry>[] buckets;
    private int bucketCount;
    private int count;

    public int Count => count;

    public MyHashMap(int capacity = 31)
    {
        bucketCount = capacity;
        buckets = new MyLinkedList<HashEntry>[bucketCount];

        for (int i = 0; i < bucketCount; i++)
        {
            buckets[i] = new MyLinkedList<HashEntry>();
        }
    }

    public void Add(TKey key, TValue value)
    {
        int bucketIndex = GetBucketIndex(key);
        MyLinkedList<HashEntry> bucket = buckets[bucketIndex];
        MyLinkedList<HashEntry>.Node current = bucket.GetHead();

        while (current != null)
        {
            if (KeysEqual(current.data.key, key))
            {
                current.data.value = value;
                return;
            }

            current = current.next;
        }

        bucket.Add(new HashEntry(key, value));
        count++;
    }

    public bool Remove(TKey key)
    {
        int bucketIndex = GetBucketIndex(key);
        MyLinkedList<HashEntry> bucket = buckets[bucketIndex];
        MyLinkedList<HashEntry> rebuiltBucket = new MyLinkedList<HashEntry>();
        bool removed = false;

        MyLinkedList<HashEntry>.Node current = bucket.GetHead();

        while (current != null)
        {
            if (KeysEqual(current.data.key, key))
            {
                removed = true;
            }
            else
            {
                rebuiltBucket.Add(current.data);
            }

            current = current.next;
        }

        if (removed)
        {
            buckets[bucketIndex] = rebuiltBucket;
            count--;
        }

        return removed;
    }

    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int bucketIndex = GetBucketIndex(key);
        MyLinkedList<HashEntry> bucket = buckets[bucketIndex];
        MyLinkedList<HashEntry>.Node current = bucket.GetHead();

        while (current != null)
        {
            if (KeysEqual(current.data.key, key))
            {
                value = current.data.value;
                return true;
            }

            current = current.next;
        }

        value = default;
        return false;
    }

    private int GetBucketIndex(TKey key)
    {
        int hash = ComputeHash(key);

        if (hash < 0)
        {
            hash = -hash;
        }

        return hash % bucketCount;
    }

    private int ComputeHash(TKey key)
    {
        if (key == null)
        {
            return 0;
        }

        if (key is string text)
        {
            int hash = 0;

            for (int i = 0; i < text.Length; i++)
            {
                hash = (hash * 31) + text[i];
            }

            return hash;
        }

        throw new System.NotSupportedException("MyHashMap only supports string keys in this project.");
    }

    private bool KeysEqual(TKey left, TKey right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        return EqualityComparer<TKey>.Default.Equals(left, right);
    }
}
