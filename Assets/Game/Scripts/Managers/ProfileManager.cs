using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-93)]
public class ProfileManager : MonoBehaviour
{
    private static ProfileManager m_Instance;
    public static ProfileManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public static PlayerProfile MyProfile
    {
        get
        {
            return m_Instance.m_LocalProfile;
        }
    }
    private PlayerProfile m_LocalProfile;

    public BigNumber m_Gold;
    public BigNumber m_Gold2 = new BigNumber(0);

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            m_Instance = this;
            InitProfile();
            DontDestroyOnLoad(gameObject);
        }

        // MyProfile.AddGold(5f);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     SetSelectedCharacter(2);
        //     Helper.DebugLog("Selected Character: " + GetSelectedCharacter());

        //     // CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(2);
        //     // Helper.DebugLog("Name " + config.m_Name);
        //     // Helper.DebugLog("Price " + config.m_Price);
        // }

        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     Helper.DebugLog("Selected Character: " + GetSelectedCharacter());
        // }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Helper.DebugLog("Keys = " + GetKeys());
        }
    }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    private void OnDestroy()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.END_GAME, PassLevel);
        // EventManagerWithParam<int>.AddListener(GameEvent.EQUIP_CHAR, EquipChar);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.END_GAME, PassLevel);
        // EventManagerWithParam<int>.RemoveListener(GameEvent.EQUIP_CHAR, EquipChar);
    }

    public void InitProfile()
    {
        CreateOrLoadLocalProfile();
    }

    private void CreateOrLoadLocalProfile()
    {
        Debug.Log("Create Or Load Data");
        LoadDataFromPref();
    }

    private void LoadDataFromPref()
    {
        Debug.Log("Load Data");
        string dataText = PlayerPrefs.GetString("SuperFetch", "");
        //Debug.Log("Data " + dataText);
        if (string.IsNullOrEmpty(dataText))
        {
            // Dont have -> create new player and save;
            CreateNewPlayer();
        }
        else
        {
            // Have -> Load data
            LoadDataToPlayerProfile(dataText);
        }
    }

    private void CreateNewPlayer()
    {
        m_LocalProfile = new PlayerProfile();
        m_LocalProfile.CreateNewPlayer();
        SaveData();
    }

    private void LoadDataToPlayerProfile(string data)
    {
        m_LocalProfile = JsonMapper.ToObject<PlayerProfile>(data);
        m_LocalProfile.LoadLocalProfile();
        m_Gold = m_LocalProfile.GetGold();
    }

    public void SaveData()
    {
        m_LocalProfile.SaveDataToLocal();
    }

    public void SaveDataText(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            PlayerPrefs.SetString("SuperFetch", data);
        }
    }
    public void TestDisplayGold()
    {
        // string a = 
        // Helper.DebugLog("Profile Gold: " + MyProfile.GetGold());
        // Helper.DebugLog("Profile Level: " + MyProfile.m_Level);
    }




    public void PassLevel()
    {
        Helper.DebugLog("profilemanager pass level");
        int level = GetLevel();
        Dictionary<int, LevelConfig> configs = GameData.Instance.GetLevelConfig();
        int pipeCount = InGameObjectsManager.Instance.m_Char.m_SpringManager.springBones.Count - 1;

        Helper.DebugLog("Pipe count: " + pipeCount);

        for (int i = 1; i <= configs.Count; i++)
        {
            if (configs[i].CheckInRange(level))
            {
                BigNumber totalGold = configs[i].m_MinGold + (1 + (level - 1) * 0.5f) * 10 * pipeCount;
                AddGold(totalGold);
                break;
            }
        }

        MyProfile.PassLevel();
    }
    public static int GetLevel()
    {
        return MyProfile.GetLevel();
    }

    public string GetLevel2()
    {
        return MyProfile.GetLevel().ToString();
    }

    public void SetLevel(int _level)
    {
        MyProfile.SetLevel(_level);
    }

    #region GENERAL
    public static string GetGold()
    {
        return MyProfile.GetGold().ToString();
    }

    public static BigNumber GetGold2()
    {
        return MyProfile.GetGold();
    }

    public static void AddGold(BigNumber _gold)
    {
        MyProfile.AddGold(_gold);
    }

    public static void SetGold(BigNumber _gold)
    {
        MyProfile.SetGold(_gold);
    }

    public static void ConsumeGold(BigNumber _gold)
    {
        MyProfile.ConsumeGold(_gold);
    }

    public static bool IsEnoughGold(BigNumber _gold)
    {
        return MyProfile.IsEnoughGold(_gold);
    }

    #endregion

    #region CHARACTER

    public static int GetSelectedCharacter()
    {
        return MyProfile.GetSelectedCharacter();
    }

    public static void SetSelectedCharacter(int _id)
    {
        MyProfile.SetSelectedCharacter(_id);
    }

    public static CharacterProfileData GetCharacterProfileData(int _id)
    {
        return MyProfile.GetCharacterProfile(_id);
    }

    public static CharacterProfileData GetCharacterProfileData(CharacterType _id)
    {
        return MyProfile.GetCharacterProfile(_id);
    }

    public static bool IsOwned(int _id)
    {
        return MyProfile.IsOwned(_id);
    }

    public void EquipChar(int _id)
    {
        MyProfile.SetSelectedCharacter(_id);
    }

    public static void UnlockNewCharacter(int _id)
    {
        MyProfile.UnlockCharacter((CharacterType)_id);
    }

    public static void UnlockNewCharacter(CharacterType _id)
    {
        MyProfile.UnlockCharacter(_id);
    }

    public int GetTotalGoldChar()
    {
        return MyProfile.GetTotalGoldChar();
    }

    public int GetTotaOwnedlGoldChar()
    {
        return MyProfile.GetTotalOwnedGoldChar();
    }

    #endregion


    #region KEYS

    public void AddKeys(BigNumber _value)
    {
        MyProfile.AddKeys(_value);
    }

    public BigNumber GetKeys()
    {
        return MyProfile.GetKeys();
    }

    #endregion

    public static int GetSelectedChar()
    {
        return MyProfile.m_SelectedCharacter;
    }

    public static bool CheckSelectedChar(int _id)
    {
        return MyProfile.CheckSelectedChar(_id);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }
}
