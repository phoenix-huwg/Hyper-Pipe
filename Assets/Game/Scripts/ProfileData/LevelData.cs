using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{

}

public class LevelConfig
{
    public int m_Id;
    public BigNumber m_MaxLevel;
    public BigNumber m_MinGold;

    public void Init(int _id, BigNumber _maxLevel, BigNumber _minGold)
    {
        m_Id = _id;
        m_MaxLevel = _maxLevel;
        m_MinGold = _minGold;
    }

    public bool CheckInRange(BigNumber _level)
    {
        return (_level <= m_MaxLevel);
    }
}
