using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
// using Newtonsoft.Json;

public class PlayerProfile
{
    private BigNumber m_Gold = new BigNumber(0);
    public string ic_Gold = "0";

    private BigNumber m_Keys = new BigNumber(0);
    public string ic_Keys = "0";

    public int m_Level;

    public int m_SelectedCharacter = 0;
    public List<CharacterProfileData> m_CharacterData = new List<CharacterProfileData>();


    public void LoadLocalProfile()
    {
        m_Gold = new BigNumber(ic_Gold);
        m_Keys = new BigNumber(ic_Keys);
        LoadCharacterData();

        // if (GetCharacterProfile(CharacterType.BATMAN) != null)
        // {
        //     Helper.DebugLog("Batman existed!!!");
        // }
        // int a = 2;
        // a = data1["m_Gold"].To;
    }

    public void CreateNewPlayer()
    {
        // PlayerPrefs.SetInt(ConfigKeys.noAds, 0);
        // PlayerPrefs.SetInt(ConfigKeys.rateUs, 1);

        ic_Gold = "9000";
        m_Gold = new BigNumber(ic_Gold);

        ic_Keys = "0";
        m_Keys = new BigNumber(ic_Keys);

        m_Level = 1;

        UnlockCharacter(CharacterType.FIREFIGHTER);
        SetSelectedCharacter(CharacterType.FIREFIGHTER);
        LoadCharacterData();
    }

    public void SaveDataToLocal()
    {
        string piJson = this.ObjectToJsonString();
        ProfileManager.Instance.SaveDataText(piJson);
    }

    public string ObjectToJsonString()
    {
        return JsonMapper.ToJson(this);
    }

    public JsonData StringToJsonObject(string _data)
    {
        return JsonMapper.ToObject(_data);
    }

    #region GOLD
    public BigNumber GetGold()
    {
        return m_Gold;
    }

    public string GetGold(bool a = false)
    {
        return (m_Gold + 1).ToString();
    }

    public bool IsEnoughGold(BigNumber _value)
    {
        // _value += 0;
        return (m_Gold >= _value);
    }

    public void AddGold(BigNumber _value)
    {
        m_Gold += _value;
        ic_Gold = m_Gold.ToString();
        // ProfileManager.Instance.SaveData();
        SaveDataToLocal();
        // EventManager.TriggerEvent("UpdateGold");
    }

    public void ConsumeGold(BigNumber _value)
    {
        m_Gold -= _value;
        ic_Gold = m_Gold.ToString();
        // ProfileManager.Instance.SaveData();
        SaveDataToLocal();
        // EventManager.TriggerEvent("UpdateGold");
    }

    public void SetGold(BigNumber _value)
    {
        m_Gold = _value;
        ic_Gold = m_Gold.ToString();
        // ProfileManager.Instance.SaveData();
        SaveDataToLocal();
        // EventManager.TriggerEvent("UpdateGold");
    }

    #endregion

    #region 

    public void AddKeys(BigNumber _value)
    {
        m_Keys += _value;
        ic_Keys = m_Keys.ToString();
        SaveDataToLocal();
    }

    public BigNumber GetKeys()
    {
        return m_Keys;
    }

    #endregion

    #region LEVEL

    public void PassLevel()
    {
        // AnalysticsManager.LogWinLevel(m_Level);

        // if (m_Level < 50)
        // {
        m_Level++;
        // }

        SaveDataToLocal();
    }

    public int GetLevel()
    {
        return m_Level;
    }

    public void SetLevel(int _level)
    {
        m_Level = _level;
        SaveDataToLocal();
    }

    #endregion

    #region CHARACTER

    public int GetSelectedCharacter()
    {
        return m_SelectedCharacter;
    }

    public void SetSelectedCharacter(int _id)
    {
        m_SelectedCharacter = _id;
        SaveDataToLocal();
        // UnlockCharacter((CharacterType)_id);
        // SetSelectedCharacter(CharacterType.BLUEBOY);
        // LoadCharacterData();

    }

    public void LoadCharacterData()
    {
        for (int i = 0; i < m_CharacterData.Count; i++)
        {
            CharacterProfileData cpd = m_CharacterData[i];
            cpd.Load();
        }
    }

    public void UnlockCharacter(CharacterType characterType)
    {
        if (GetCharacterProfile(characterType) == null)
        {
            CharacterProfileData newCharacter = new CharacterProfileData();
            newCharacter.Init(characterType);
            newCharacter.Load();
            m_CharacterData.Add(newCharacter);
        }
    }

    public void SetSelectedCharacter(CharacterType characterType)
    {
        m_SelectedCharacter = (int)characterType;
    }

    public CharacterProfileData GetCharacterProfile(int characterType)
    {
        return GetCharacterProfile((CharacterType)characterType);
    }

    public CharacterProfileData GetCharacterProfile(CharacterType characterType)
    {
        for (int i = 0; i < m_CharacterData.Count; i++)
        {
            CharacterProfileData cpd = m_CharacterData[i];
            if (cpd.m_Cid == characterType)
            {
                return cpd;
            }
        }
        return null;
    }

    public bool IsOwned(int _id)
    {
        CharacterProfileData data = GetCharacterProfile(_id);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

        if (data != null)
        {
            if (config.CheckAds())
            {
                if (data.m_AdsNumber >= config.m_AdsNumber)
                {
                    Helper.DebugLog("data.m_AdsNumber: " + data.m_AdsNumber);
                    Helper.DebugLog("config.m_AdsNumber: " + config.m_AdsNumber);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public int GetTotalGoldChar()
    {
        int total = 0;

        Dictionary<int, CharacterDataConfig> configs = GameData.Instance.GetCharacterDataConfig();

        for (int i = 1; i < configs.Count + 1; i++)
        {
            // CharacterProfileData data = ProfileManager.GetCharacterProfileData(configs[i].m_Id);

            // if (data == null)
            // {
            if (configs[i].m_AdsCheck == 0)
            {
                total++;
            }
            // }
        }

        return total;
    }

    public int GetTotalOwnedGoldChar()
    {
        int total = 0;

        Dictionary<int, CharacterDataConfig> configs = GameData.Instance.GetCharacterDataConfig();

        for (int i = 1; i < configs.Count + 1; i++)
        {
            if (IsOwned(configs[i].m_Id))
            {
                if (configs[i].m_AdsCheck == 0)
                {
                    total++;
                }
            }
        }

        return total;
    }

    public bool CheckSelectedChar(int _id)
    {
        if (_id == m_SelectedCharacter)
        {
            return true;
        }

        return false;
    }

    #endregion
}