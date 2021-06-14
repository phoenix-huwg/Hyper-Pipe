using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[DefaultExecutionOrder(-95)]
public class GameData : Singleton<GameData>
{
    public List<TextAsset> m_DataText = new List<TextAsset>();

    private Dictionary<int, CharacterDataConfig> m_CharacterDataConfigs = new Dictionary<int, CharacterDataConfig>();
    private Dictionary<int, LevelConfig> m_LevelConfigs = new Dictionary<int, LevelConfig>();

    private void Awake()
    {
        LoadCharacterConfig();
        LoadLevelConfig();

        // CharacterDataConfig charrr = GetCharacterDataConfig(CharacterType.FIREMAN);

        // Helper.DebugLog("m_Id: " + charrr.m_Id);
        // Helper.DebugLog("m_Name: " + charrr.m_Name);
        // Helper.DebugLog("m_RunSpeed: " + charrr.m_RunSpeed);
        // Helper.DebugLog("m_Price: " + charrr.m_Price);
        // Helper.DebugLog("m_AdsCheck: " + charrr.m_AdsCheck);
        // Helper.DebugLog("m_AdsNumber: " + charrr.m_AdsNumber);

        // Dictionary<int, CharacterDataConfig> characterDataConfig = GameData.Instance.GetCharacterDataConfig();

        // Helper.DebugLog("Name: " + characterDataConfig[0].m_Name);
    }

    public void LoadCharacterConfig()
    {
        m_CharacterDataConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.DATA_CHAR);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["ID"]);

            string name = "";
            if (iNode["Name"].ToString().Length > 0)
            {
                name = iNode["Name"];
            }

            string colName = "";

            BigNumber price = 0;
            colName = "Price";
            if (iNode[colName].ToString().Length > 0)
            {
                price = new BigNumber(iNode[colName]) + 0;
            }

            int adsCheck = 0;
            colName = "AdsCheck";
            if (iNode[colName].ToString().Length > 0)
            {
                adsCheck = int.Parse(iNode[colName]);
            }

            int adsNumber = 0;
            colName = "AdsNumber";
            if (iNode[colName].ToString().Length > 0)
            {
                adsNumber = int.Parse(iNode[colName]);
            }

            int rarity = 0;
            colName = "Rarity";
            if (iNode[colName].ToString().Length > 0)
            {
                rarity = int.Parse(iNode[colName]);
            }

            CharacterDataConfig character = new CharacterDataConfig();
            character.Init(id, name, price, adsCheck, adsNumber, rarity);
            m_CharacterDataConfigs.Add(id, character);
        }
    }

    public void LoadLevelConfig()
    {
        m_LevelConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.LEVEL_CONfIG);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["ID"]);

            string colName = "";

            BigNumber maxLevel = 0;
            colName = "MaxLevel";
            if (iNode[colName].ToString().Length > 0)
            {
                maxLevel = new BigNumber(iNode[colName]) + 0;
            }

            BigNumber minGold = 0;
            colName = "MinGold";
            if (iNode[colName].ToString().Length > 0)
            {
                minGold = new BigNumber(iNode[colName]) + 0;
            }

            LevelConfig levelConfig = new LevelConfig();
            levelConfig.Init(id, maxLevel, minGold);
            m_LevelConfigs.Add(id, levelConfig);
        }
    }

    public TextAsset GetDataAssets(GameDataType _id)
    {
        return m_DataText[(int)_id];
    }

    public CharacterDataConfig GetCharacterDataConfig(int charID)
    {
        return m_CharacterDataConfigs[charID];
    }
    public CharacterDataConfig GetCharacterDataConfig(CharacterType characterType)
    {
        return m_CharacterDataConfigs[(int)characterType];
    }
    public Dictionary<int, CharacterDataConfig> GetCharacterDataConfig()
    {
        return m_CharacterDataConfigs;
    }

    public Dictionary<int, LevelConfig> GetLevelConfig()
    {
        return m_LevelConfigs;
    }

    public enum GameDataType
    {
        DATA_CHAR = 0,
        LEVEL_CONfIG = 1,
    }
}