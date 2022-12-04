using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static volatile T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                GameObject gObj = new GameObject();
                gObj.name = typeof(T).Name;
                gObj.AddComponent<T>();
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            if (transform.parent != null) { transform.SetParent(null); }
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
