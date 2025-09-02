using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object _lock = new object();
    public static bool IsShuttingDown { get; private set; } = false;

    public static T Instance
    {
        get
        {
            if (IsShuttingDown) return null;

            lock (_lock)
            {
                if (instance == null)
                {
                    // Tìm trong scene
                    instance = (T)FindObjectOfType(typeof(T));

                    // Nếu chưa có thì tự tạo
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        IsShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            IsShuttingDown = true;
    }
}