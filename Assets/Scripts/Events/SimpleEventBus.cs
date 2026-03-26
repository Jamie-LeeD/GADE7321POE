using System.Collections.Generic;
using UnityEngine;

public class SimpleEventBus : MonoBehaviour
{
    public static SimpleEventBus Instance;

    private Dictionary<GameEventType, List<ISimpleListener>> listeners = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddListener(GameEventType eventType, ISimpleListener listener)
    {
        if (listener == null) return;

        if (!listeners.TryGetValue(eventType, out var list))
        {
            list = new List<ISimpleListener>();
            listeners[eventType] = list;
        }

        if (!list.Contains(listener))
        {
            list.Add(listener);
        }
    }

    public void RemoveListener(GameEventType eventType, ISimpleListener listener)
    {
        if (listeners.TryGetValue(eventType, out var list))
        {
            list.Remove(listener);

            if (list.Count == 0)
            {
                listeners.Remove(eventType);
            }
        }
    }

    public void PostNotification(GameEventType eventType, object sender, object param = null)
    {
        if (!listeners.TryGetValue(eventType, out var list)) return;

        for (int i = list.Count - 1; i >= 0; i--)
        {
            list[i]?.OnEvent(eventType, sender, param);
        }
    }
}