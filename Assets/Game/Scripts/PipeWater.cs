using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWater : Pipe
{
    public GameObject g_Water;

    public override void OnEnable()
    {
        g_Water.SetActive(false);
        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager1<bool>.AddListener(GameEvent.WATER, Event_WATER);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager1<bool>.RemoveListener(GameEvent.WATER, Event_WATER);
    }

    public void Event_WATER(bool _value)
    {
        g_Water.SetActive(_value);
    }
}
