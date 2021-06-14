using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    instance.gameObject.name = instance.GetType().Name;
                }
            }
            return instance;
        }
    }

    public virtual void OnEnable()
    {
        StartListenToEvents();
    }

    public virtual void OnDisable()
    {
        StopListenToEvents();
    }

    public virtual void OnDestroy()
    {
        StopListenToEvents();
    }

    public virtual void StartListenToEvents()
    {

    }

    public virtual void StopListenToEvents()
    {

    }
}
