using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCell : InGameObject
{
    public List<GameObject> g_Grounds;
    public GameObject g_KeyInGame;

    public override void OnEnable()
    {
        if (g_KeyInGame != null)
        {
            g_KeyInGame.SetActive(false);
        }
        base.OnEnable();
    }

    public override void StartListenToEvents()
    {
        EventManager.AddListener(GameEvent.LOAD_MAP, DestroyPath);
    }

    public override void StopListenToEvents()
    {
        EventManager.RemoveListener(GameEvent.LOAD_MAP, DestroyPath);
    }

    public float CalculateTotalLength()
    {
        float length = 0f;
        for (int i = 0; i < g_Grounds.Count; i++)
        {
            Ground gc = g_Grounds[i].GetComponent<Ground>();
            if (gc != null)
            {
                // length += (col.size.z - 2f);
                // length += (col.size.z - 0.5f);
                length += (gc.m_GroundCells.Count * 8f - 0.5f);
            }
            else
            {
                length += (g_Grounds[i].transform.localScale.z * 10f);
            }
        }

        return length;
    }

    public void DestroyPath()
    {
        Destroy(gameObject);
    }
}
