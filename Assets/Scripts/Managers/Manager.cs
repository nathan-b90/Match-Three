using UnityEngine;

/// <summary>
/// Base class for managers.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Manager<T> : MonoBehaviour, IManager where T : Manager<T>
{
    public static T Instance;

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}