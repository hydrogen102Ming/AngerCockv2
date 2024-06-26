using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance is null) _instance = Initialize();
            return _instance;
        }
    }
    private static T Initialize()
    {
        GameObject gameObject = new GameObject();
        gameObject.name = "Singleton_" + typeof(T).Name;
        T result = gameObject.AddComponent<T>();
        return result;
    }
    protected virtual void Awake()
    {
        if (_instance is not null)
        {
            Debug.LogError("twoSingletons");
            Destroy(gameObject);
            return;
        }
        _instance = this as T;
    }
    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}