using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[DefaultExecutionOrder(-92)]
public class InGameObjectsManager : Singleton<InGameObjectsManager>
{
    public Character m_Char;
    public float m_MapLength;

    public GameObject g_Map;

    public House m_House;

    public int m_MapMinConfig;
    public int m_MapMaxConfig;

    public int m_MapPrefabMinConfig;
    public int m_MapPrefabMaxConfig;

    public int m_MapMin;
    public int m_MapMax;

    public int m_MapPrefabMin;
    public int m_MapPrefabMax;

    public List<LevelMapConfig> m_LevelConfigs;

    public GameObject g_Ending;

    public int m_ScoreLine;

    public List<GameObject> g_GoldEffects = new List<GameObject>();

    public override void OnEnable()
    {
        base.OnEnable();
        m_ScoreLine = 1;
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
    }

    public override void StopListenToEvents()
    {
        base.StopListenToEvents();
        EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
    }

    public void Event_END_GAME()
    {
        m_ScoreLine = 1;
    }

    public void DespawnAllPools()
    {
        DespawnGoldEffectPool();
    }

    public void DespawnGoldEffectPool()
    {
        int count = g_GoldEffects.Count;
        for (int i = 0; i < count; i++)
        {
            // g_GoldEffects.Dokill
            g_GoldEffects[i].transform.DOKill();
            PrefabManager.Instance.DespawnPool(g_GoldEffects[i]);
        }
        g_GoldEffects.Clear();
    }

    public void LoadMap()
    {
        EventManager.CallEvent(GameEvent.LOAD_MAP);

        CameraController.Instance.g_Wind.SetActive(false);

        Score.m_Score = 0;
        m_MapLength = 0;

        int level = ProfileManager.GetLevel();

        if (level <= 10)
        {
            m_MapMin = m_LevelConfigs[level - 1].m_MapMin;
            m_MapMax = m_LevelConfigs[level - 1].m_MapMax;
            m_MapPrefabMin = m_LevelConfigs[level - 1].m_MapPrefabMin;
            m_MapPrefabMax = m_LevelConfigs[level - 1].m_MapPrefabMax;
        }
        else
        {
            m_MapMin = m_MapMinConfig;
            m_MapMax = m_MapMaxConfig;
            m_MapPrefabMin = m_MapPrefabMinConfig;
            m_MapPrefabMax = m_MapPrefabMaxConfig;
        }

        int mapLengthRandom = Random.Range(m_MapMin, m_MapMax + 1);

        List<GameObject> keysInGame = new List<GameObject>();

        for (int i = 0; i < mapLengthRandom; i++)
        {
            int mapCellRandom = Random.Range(m_MapPrefabMin - 1, m_MapPrefabMax);
            GameObject go = PrefabManager.Instance.SpawnPathCell(mapCellRandom, m_MapLength);
            go.transform.parent = PlaySceneManager.Instance.g_Map.transform;
            PathCell cell = go.GetComponent<PathCell>();
            if (cell.g_KeyInGame != null)
            {
                keysInGame.Add(cell.g_KeyInGame);
            }
            m_MapLength += cell.CalculateTotalLength();
        }


        if ((level % GameManager.Instance.m_KeyInGameStep) == 1)
        {
            keysInGame[Random.Range(0, keysInGame.Count)].SetActive(true);
        }

        GameObject ending = PrefabManager.Instance.SpawnEnding(m_MapLength - 21f);
        g_Ending = ending;
        ending.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
        m_MapLength += ending.GetComponent<BoxCollider>().size.y * 10f;

        for (int i = 0; i < 20; i++)
        {
            if (i == 0)
            {
                GameObject go = PrefabManager.Instance.SpawnScoreLine(ConfigKeys.m_ScoreLine, new Vector3(0f, 0f, ending.transform.position.z + 4f + 1f));
                go.GetComponent<Score>().SetScore(m_ScoreLine, i + 1, GameManager.Instance.m_ScoreLineColor[i % 7]);
                m_ScoreLine++;
            }
            else
            {
                GameObject go = PrefabManager.Instance.SpawnScoreLine(ConfigKeys.m_ScoreLine, new Vector3(0f, 0f, ending.transform.position.z + i * 2 * 2 + 4f + i * 2f));
                go.GetComponent<Score>().SetScore(m_ScoreLine, i + 1, GameManager.Instance.m_ScoreLineColor[i % 7]);
                m_ScoreLine++;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject go = PrefabManager.Instance.SpawnPathCell0(m_MapLength - 5f);
            go.transform.parent = PlaySceneManager.Instance.g_Map.transform;
            PathCell cell = go.GetComponent<PathCell>();
            m_MapLength += cell.CalculateTotalLength();
        }

        GameObject truck = PrefabManager.Instance.SpawnTruck(0);

        CameraController.Instance.m_CMFreeLook.Follow = truck.transform;
        truck.transform.DOMove(new Vector3(0f, 0f, -52.16f), 2.5f).OnComplete
        (
            () =>
            {
                // EventManager1<bool>.CallEvent(GameEvent.GAME_START, true);
                PlaySceneManager.Instance.btn_Outfit.gameObject.SetActive(true);
                int index = ProfileManager.GetSelectedChar();
                GameObject charrr = PrefabManager.Instance.SpawnChar(index - 1);
                m_Char = charrr.GetComponent<Character>();
                CameraController.Instance.m_CMFreeLook.Follow = charrr.transform;
                truck.transform.DOMove(new Vector3(0f, 0f, -77.97f), 1.5f);
            }
        );
    }

    public void LoadChar(int _characterType)
    {
        if (m_Char != null)
        {
            Helper.DebugLog("3333333333333333333333333333");
            Destroy(m_Char.gameObject);
            m_Char = null;
            int index = ProfileManager.GetSelectedChar();
            GameObject charrr = PrefabManager.Instance.SpawnChar(index - 1);
            m_Char = charrr.GetComponent<Character>();
            CameraController.Instance.m_CMFreeLook.Follow = charrr.transform;
        }
        // else
        // {
        //     ProfileManager.SetSelectedCharacter(_characterType);
        // }
    }
}

[System.Serializable]
public struct LevelMapConfig
{
    public int m_MapMin;
    public int m_MapMax;

    public int m_MapPrefabMin;
    public int m_MapPrefabMax;
}