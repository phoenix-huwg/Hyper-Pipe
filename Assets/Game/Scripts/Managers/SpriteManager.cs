using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : Singleton<SpriteManager>
{
    public Sprite[] m_CharCards;
    public Sprite[] m_UICardBG;
}

public enum MiscSpriteKeys
{
    UI_CARD_BG_LOCK = 0,
    UI_CARD_BG_UNLOCK = 1,
}