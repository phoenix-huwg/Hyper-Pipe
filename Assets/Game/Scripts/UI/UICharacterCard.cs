using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class UICharacterCard : MonoBehaviour, ICell
{
    //UI
    // public TextMeshProUGUI m_Name;
    public Button btn_LoadChar;
    public Image img_Char;
    public Image img_BG;
    public TextMeshProUGUI txt_Price;
    public TextMeshProUGUI txt_AdsClaim;

    public GameObject g_SelectedOutline;
    public GameObject g_Owned;
    public GameObject g_Equipped;
    public GameObject g_Price;
    public GameObject g_AdsClaim;
    public GameObject g_Lock;

    //Model
    public UICharacterCardInfo m_UICharacterCardInfo;
    // public int _cellIndex;

    private void Awake()
    {
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[m_UIChar];
        GUIManager.Instance.AddClickEvent(btn_LoadChar, Click);
        // Helper.DebugLog
    }

    private void OnEnable()
    {
        // CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);
        // Helper.DebugLog("m_UICharacterCardInfo.m_Id: " + m_UICharacterCardInfo.m_Id);
        // g_SelectedOutline.SetActive(ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id));
        // Event_LOAD_CHAR_OUTFIT(m_UICharacterCardInfo.m_Id);

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
        // EventManagerWithParam<int>.AddListener(GameEvents.EQUIP_CHAR, SetEquippedChar);
        // EventManagerWithParam<int>.AddListener(GameEvents.CLAIM_CHAR, OnUpdateAdsNumber);
        // EventManager.AddListener(GameEvents.UI_CARD_SET_SELECT_CHAR, OnSetSelectedCharacter);
        EventManager1<int>.AddListener(GameEvent.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
        // EventManager.AddListener(GameEvents.TEST_UPDATE_NEW_OUTFIT, Event_TEST_UPDATE_NEW_OUTFIT);
    }

    public void StopListenToEvent()
    {
        // EventManagerWithParam<int>.RemoveListener(GameEvents.EQUIP_CHAR, SetEquippedChar);
        // EventManagerWithParam<int>.RemoveListener(GameEvents.CLAIM_CHAR, OnUpdateAdsNumber);
        // EventManager.RemoveListener(GameEvents.UI_CARD_SET_SELECT_CHAR, OnSetSelectedCharacter);
        EventManager1<int>.RemoveListener(GameEvent.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
        // EventManager.RemoveListener(GameEvents.TEST_UPDATE_NEW_OUTFIT, Event_TEST_UPDATE_NEW_OUTFIT);
    }

    // public void Event_TEST_UPDATE_NEW_OUTFIT()
    // {
    //     Destroy(gameObject);
    // }

    // //This is called from the SetCell method in DataSource
    public void ConfigureCell(UICharacterCardInfo _info, int cellIndex)
    {
        // _cellIndex = cellIndex;
        m_UICharacterCardInfo = _info;

        CharacterProfileData data = ProfileManager.GetCharacterProfileData(_info.m_Id);

        if (data != null)
        {
            txt_AdsClaim.text = data.m_AdsNumber.ToString() + "/" + _info.m_AdsNumber.ToString();
        }
        else
        {
            txt_AdsClaim.text = "0" + "/" + _info.m_AdsNumber.ToString();
        }

        // m_Name.text = _info.m_Name;
        txt_Price.text = _info.m_Price;

        img_Char.sprite = SpriteManager.Instance.m_CharCards[_info.m_Id - 1];
        // // img_Char.sprite = SpriteManager.Instance.m_CharCard.GetSprite(_info.m_Name);

        g_SelectedOutline.SetActive(_info.m_Id == PopupOutfit.m_SelectedCharacter);

        SetCellStatus();
    }

    // public bool CheckTutorial()
    // {
    //     if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    public void SetCellStatus()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);
        // txt_SelectChar = m_UICharacterCardInfo.m_Id;
        if (ProfileManager.IsOwned(m_UICharacterCardInfo.m_Id))
        {
            if (ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id))
            {
                // g_Equipped.SetActive(true);
                // g_Owned.SetActive(false);
            }
            else
            {
                // g_Equipped.SetActive(false);
                // g_Owned.SetActive(true);
            }

            // g_Price.SetActive(false);
            // g_AdsClaim.SetActive(false);
            g_Lock.SetActive(false);
            img_BG.sprite = SpriteManager.Instance.m_UICardBG[m_UICharacterCardInfo.m_Rarity];
        }
        else
        {
            // g_Owned.SetActive(false);
            // g_Equipped.SetActive(false);
            g_Lock.SetActive(true);

            bool adsCheck;

            if (config.m_AdsCheck == 1)
            {
                adsCheck = true;
            }
            else
            {
                adsCheck = false;
            }

            if (config.GetRatity() == (int)OutfitRarity.LEGEND)
            {
                // g_Price.SetActive(false);
                // g_AdsClaim.SetActive(false);
            }
            else
            {
                // g_Price.SetActive(!adsCheck);
                // g_AdsClaim.SetActive(adsCheck);
            }
            img_BG.sprite = SpriteManager.Instance.m_UICardBG[0];
        }

        // if (m_UICharacterCardInfo.m_Id == 2)
        // {
        //     // if (CheckTutorial())
        //     if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
        //     {
        //         PopupCaller.OpenTutorialPopup(false);
        //         PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_ClickCharUI(GetComponent<RectTransform>());
        //     }
        // }
    }

    private void Click()
    {
        // // MiniCharacterStudio.Instance.SetChar(m_UICharacterCardInfo.m_Id);
        EventManager1<int>.CallEvent(GameEvent.LOAD_CHAR_OUTFIT, m_UICharacterCardInfo.m_Id);
        // ProfileManager.UnlockNewCharacter(m_UICharacterCardInfo.m_Id);
        // ProfileManager.SetSelectedCharacter(m_UICharacterCardInfo.m_Id);
        // InGameObjectsManager.Instance.LoadChar(m_UICharacterCardInfo.m_Id);
        // EventManager.CallEvent(GameEvent.UI_CARD_SET_SELECT_CHAR);
        // EventManager.CallEvent(GameEvents.LOAD_CHAR_OUTFIT);

        // g_SelectedOutline.SetActive(m_UICharacterCardInfo.m_Id == PopupOutfit.m_SelectedCharacter);

        // // if (CheckTutorial.TutorialType.)
        // // {

        // // }

        // if (m_UICharacterCardInfo.m_Id == 2)
        // {
        //     if (CheckTutorial())
        //     {
        //         PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_UnClickCharUI(GetComponent<RectTransform>());
        //     }
        // }
    }

    private void Event_LOAD_CHAR_OUTFIT(int _index)
    {
        g_SelectedOutline.SetActive(m_UICharacterCardInfo.m_Id == _index);
        // g_SelectedOutline.SetActive(true);
    }

    public void SetUpFirstTime()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);
        // txt_SelectChar = m_UICharacterCardInfo.m_Id;
        if (ProfileManager.IsOwned(m_UICharacterCardInfo.m_Id))
        {
            if (ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id))
            {
                // g_Equipped.SetActive(true);
                // g_Owned.SetActive(false);
            }
            else
            {
                // g_Equipped.SetActive(false);
                // g_Owned.SetActive(true);
            }

            // g_Price.SetActive(false);
            // g_AdsClaim.SetActive(false);
            g_Lock.SetActive(false);
            img_BG.sprite = SpriteManager.Instance.m_UICardBG[m_UICharacterCardInfo.m_Rarity];
        }
        else
        {
            // g_Owned.SetActive(false);
            // g_Equipped.SetActive(false);
            g_Lock.SetActive(true);

            bool adsCheck;

            if (config.m_AdsCheck == 1)
            {
                adsCheck = true;
            }
            else
            {
                adsCheck = false;
            }

            // g_Price.SetActive(!adsCheck);
            // g_AdsClaim.SetActive(adsCheck);
            img_BG.sprite = SpriteManager.Instance.m_UICardBG[0];
        }

        // if (m_UICharacterCardInfo.m_Id == 2)
        // {
        //     // if (CheckTutorial())
        //     if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
        //     {
        //         PopupCaller.OpenTutorialPopup(false);
        //         PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_ClickCharUI(GetComponent<RectTransform>());
        //     }
        // }
    }

    // public void OnUpdateAdsNumber(int _id)
    // {
    //     if (_id == m_UICharacterCardInfo.m_Id)
    //     {
    //         SetCellStatus();

    //         SetEquippedChar(_id);

    //         CharacterProfileData data = ProfileManager.GetCharacterProfileData(_id);
    //         CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

    //         txt_AdsClaim.text = data.m_AdsNumber.ToString() + "/" + config.m_AdsNumber.ToString();
    //     }
    //     // else
    //     // {
    //     //     g_SelectedOutline.SetActive(false);
    //     // }
    // }

    // public void SetEquippedChar(int _id)
    // {
    //     CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
    //     CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);

    //     g_SelectedOutline.SetActive(ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id));

    //     SetCellStatus();
    // }
}

[System.Serializable]
public class UICharacterCardInfo
{
    public int m_Id;
    public string m_Name;
    public string m_Price;
    public int m_AdsNumber;
    public int m_Rarity;
}