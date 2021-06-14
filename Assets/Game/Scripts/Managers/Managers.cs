using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers m_Instance;
    public static Managers Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}