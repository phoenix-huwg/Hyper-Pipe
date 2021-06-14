using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    private Dictionary<string, GameObject> m_IngameObjectPrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_IngameObjectPrefabs;

    private Dictionary<string, GameObject> m_PipePrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_PipePrefabs;

    private Dictionary<string, GameObject> m_ScoreLinePrefabDict = new Dictionary<string, GameObject>();

    private Dictionary<string, GameObject> m_GoldEffectPrefabDict = new Dictionary<string, GameObject>();
    public GameObject[] m_GoldEffectPrefabs;

    public GameObject[] m_ScoreLinePrefabs;

    public GameObject[] m_PathCellPrefabs;

    public GameObject[] m_CharPrefabs;

    public GameObject[] m_TruckPrefabs;

    public GameObject g_Ending;

    public GameObject g_PathCell_0;

    public GameObject[] g_Houses;

    private void Awake()
    {
        InitPrefab();
        InitIngamePrefab();
    }

    public void InitPrefab()
    {
        for (int i = 0; i < m_PipePrefabs.Length; i++)
        {
            GameObject iPrefab = m_PipePrefabs[i];
            if (iPrefab == null) continue;
            string iName = iPrefab.name;
            try
            {
                m_PipePrefabDict.Add(iName, iPrefab);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        for (int i = 0; i < m_ScoreLinePrefabs.Length; i++)
        {
            GameObject iPrefab = m_ScoreLinePrefabs[i];
            if (iPrefab == null) continue;
            string iName = iPrefab.name;
            try
            {
                m_ScoreLinePrefabDict.Add(iName, iPrefab);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        for (int i = 0; i < m_GoldEffectPrefabs.Length; i++)
        {
            GameObject iPrefab = m_GoldEffectPrefabs[i];
            if (iPrefab == null) continue;
            string iName = iPrefab.name;
            try
            {
                m_GoldEffectPrefabDict.Add(iName, iPrefab);
            }
            catch (System.Exception)
            {
                continue;
            }
        }
    }

    public void InitIngamePrefab()
    {
        string pipe1 = ConfigKeys.m_Pipe1.ToString();
        CreatePool(pipe1, GetPipePrefabByName(pipe1), 10);
        string pipe0 = ConfigKeys.m_Pipe0.ToString();
        CreatePool(pipe0, GetPipePrefabByName(pipe0), 2);

        string scoreLine = ConfigKeys.m_ScoreLine.ToString();
        CreatePool(scoreLine, GetScoreLinePrefabByName(scoreLine), 10);

        string goldEffect = ConfigKeys.m_GoldEffect1.ToString();
        CreatePool(goldEffect, GetGoldEffectPrefabByName(goldEffect), 10);
    }

    public void CreatePool(string name, GameObject prefab, int amount)
    {
        SimplePool.Preload(prefab, amount, name);
    }

    public GameObject SpawnPool(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetPrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject GetPrefabByName(string name)
    {
        GameObject rPrefab = null;
        if (m_IngameObjectPrefabDict.TryGetValue(name, out rPrefab))
        {
            return rPrefab;
        }
        return null;
    }

    public GameObject GetPipePrefabByName(string name)
    {
        if (m_PipePrefabDict.ContainsKey(name))
        {
            return m_PipePrefabDict[name];
        }
        return null;
    }

    public GameObject SpawnPipePool(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetPipePrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject GetScoreLinePrefabByName(string name)
    {
        if (m_ScoreLinePrefabDict.ContainsKey(name))
        {
            return m_ScoreLinePrefabDict[name];
        }
        return null;
    }

    public GameObject SpawnScoreLine(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetScoreLinePrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject GetGoldEffectPrefabByName(string name)
    {
        if (m_GoldEffectPrefabDict.ContainsKey(name))
        {
            return m_GoldEffectPrefabDict[name];
        }
        return null;
    }

    public GameObject SpawnGoldEffect(string name, Vector3 pos)
    {
        if (SimplePool.IsHasPool(name))
        {
            GameObject go = SimplePool.Spawn(name, pos, Quaternion.identity);
            return go;
        }
        else
        {
            GameObject prefab = GetGoldEffectPrefabByName(name);
            if (prefab != null)
            {
                SimplePool.Preload(prefab, 1, name);
                GameObject go = SpawnPool(name, pos);
                return go;
            }
        }
        return null;
    }

    public GameObject SpawnPathCell(int _index, float _zPos)
    {
        GameObject cell = Instantiate(m_PathCellPrefabs[_index], new Vector3(0f, 0f, _zPos), Quaternion.identity);
        return cell;
    }

    public GameObject SpawnChar(int _index)
    {
        return Instantiate(m_CharPrefabs[_index], new Vector3(-0.2f, 0f, -44.2f), Quaternion.identity);
    }

    public GameObject SpawnTruck(int _index)
    {
        return Instantiate(m_TruckPrefabs[_index], new Vector3(-0f, 0f, -77.97f), Quaternion.identity);
    }

    public GameObject SpawnEnding(float _zPos)
    {
        return Instantiate(g_Ending, new Vector3(0f, 0f, _zPos), Quaternion.identity);
    }

    public GameObject SpawnPathCell0(float _zPos)
    {
        return Instantiate(g_PathCell_0, new Vector3(0f, 0f, _zPos), Quaternion.identity);
    }

    public GameObject SpawnHouse(float _zPos)
    {
        return Instantiate(g_Houses[0], new Vector3(0f, g_Houses[0].transform.position.y, _zPos), Quaternion.identity);
    }

    // public GameObject ScoreLine(float _zPos)
    // {
    //     return Instantiate(g_ScoreLine, new Vector3(0f, 0f, _zPos), Quaternion.identity);
    // }

    // public GameObject SpawnCharacter(Vector3 _pos, int _index)
    // {
    //     return Instantiate(m_CharPrefabs[_index], _pos, Quaternion.identity);
    // }

    // public GameObject SpawnMiniCharacterStudio(Vector3 _pos, int _index)
    // {
    //     return Instantiate(m_MiniCharPrefabs[_index - 1], _pos, Quaternion.identity);
    // }

    public void DespawnPool(GameObject go)
    {
        SimplePool.Despawn(go);
    }
}
