using UnityEngine;

public interface ISimpleListener
{
    void OnEvent(GameEventType eventType, object sender, object param = null);
}