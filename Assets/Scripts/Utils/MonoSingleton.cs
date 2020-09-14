using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    private bool _alive = true;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        singleton.transform.parent = GameManager.instance.transform;

                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).Name;
                    }
                }

                return _instance;
            }
        }
    }

    public static bool IsAlive
    {
        get
        {
            if (_instance == null)
                return false;
            return _instance._alive;
        }
    }

    protected virtual void OnDestroy()
    {
        _alive = false;
    }

    protected virtual void OnApplicationQuit()
    {
        _alive = false;
    }
}
