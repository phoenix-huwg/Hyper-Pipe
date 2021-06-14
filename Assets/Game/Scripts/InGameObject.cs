using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObject : MonoBehaviour
{
    public Transform tf_Owner;

    private void Awake()
    {
        tf_Owner = GetComponent<Transform>();
    }

    public virtual void OnEnable()
    {
        StartListenToEvents();
    }

    public void OnDisable()
    {
        StopListenToEvents();
    }

    public void OnDestroy()
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
