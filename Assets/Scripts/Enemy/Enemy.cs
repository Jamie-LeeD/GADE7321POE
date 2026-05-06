using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public int damage;

    public abstract void Initialize();
}