using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileData
{
    public CharacterType m_Cid;
    public string m_Name;
    public int m_AdsNumber;

    public void Init(CharacterType _id)
    {
        m_Cid = _id;
        m_AdsNumber = 0;
    }

    public void Load()
    {
        CharacterDataConfig cdc = GameData.Instance.GetCharacterDataConfig(m_Cid);
        m_Name = cdc.m_Name;
    }

    public void ClaimByAds(int _value)
    {
        m_AdsNumber += _value;
        ProfileManager.Instance.SaveData();
    }
}

public class CharacterDataConfig
{
    public int m_Id;
    public string m_Name;
    public BigNumber m_Price;
    public int m_AdsCheck;
    public int m_AdsNumber;
    public int m_IsRarity;

    public void Init(int _id, string _name, BigNumber _price, int _adsCheck, int _adsNumber, int _isRarity)
    {
        m_Id = _id;
        m_Name = _name;
        m_Price = _price;
        m_AdsCheck = _adsCheck;
        m_AdsNumber = _adsNumber;

        m_IsRarity = _isRarity;
    }

    public bool CheckAds()
    {
        if (m_AdsCheck == 1)
        {
            return true;
        }

        return false;
    }

    public int GetRatity()
    {
        return m_IsRarity;
    }
}

public enum CharacterType
{
    FIREFIGHTER = 1,
    FIREFIGHTER1 = 2,
    FIREFIGHTER2 = 3,
    FIREFIGHTER3 = 4,
    FLASH = 5,
    SANTA = 6,
    TELEVISIONMAN = 7,
    WIZARD = 8,
    WORKER = 9,
    ASTRONAUS = 10,
    BATBOY = 11,
    BEAR = 12,
    BLUEBOY = 13,
    BUSINESSMAN = 14,
    CLOWN = 15,
    COWBOY = 16,
    GREEENBOY = 17,
    MOHAWKBOY = 18,
    STREETBOY = 19,
    SUPERBOY = 20,
    VIKING = 21,
}