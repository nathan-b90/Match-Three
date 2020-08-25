using UnityEngine;

/// <summary>
/// Base class for utilities that can persist between scenes.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Utility<T> : MonoBehaviour, IUtility where T : Utility<T>
{
    [SerializeField] private bool persistScene;

    public static T Instance;

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;

            if (persistScene)
                DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}