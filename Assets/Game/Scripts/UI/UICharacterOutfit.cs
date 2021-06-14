using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion

public class UICharacterOutfit : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<UICharacterCardInfo> _contactList = new List<UICharacterCardInfo>();

    public RectTransform rect_Content;

    public Transform tf_Content;

    [Header("Rarity buttons")]
    public OutfitRarity m_OutfitRarity;
    public Button btn_Rare;
    public Button btn_Epic;
    public Button btn_Legend;
    public Image img_RarityBG;
    public Color[] m_RarityBG;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        // InitCell();
        // // // MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(ProfileManager.GetSelectedCharacter());
        // _recyclableScrollRect.DataSource = this;

        GUIManager.Instance.AddClickEvent(btn_Rare, () =>
        {
            m_OutfitRarity = OutfitRarity.RARE;
            Event_UPDATE_OUTFIT();
        });

        GUIManager.Instance.AddClickEvent(btn_Epic, () =>
        {
            m_OutfitRarity = OutfitRarity.EPIC;
            Event_UPDATE_OUTFIT();
        });

        GUIManager.Instance.AddClickEvent(btn_Legend, () =>
        {
            m_OutfitRarity = OutfitRarity.LEGEND;
            Event_UPDATE_OUTFIT();
        });
    }

    private void OnEnable()
    {
        // _recyclableScrollRect.Initialize();
        // InitCell();
        // MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(ProfileManager.GetSelectedCharacter());
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(ProfileManager.GetSelectedChar());

        m_OutfitRarity = (OutfitRarity)config.GetRatity();

        _recyclableScrollRect.DataSource = this;
        Event_UPDATE_OUTFIT();
        // EventManager.CallEvent(GameEvent.UPDATE_OUTFIT);
        StartListenToEvents();
    }

    private void OnDisable()
    {
        StopListenToEvents();
    }

    private void OnDestroy()
    {
        StopListenToEvents();
    }

    public void StartListenToEvents()
    {
        EventManager.AddListener(GameEvent.UPDATE_OUTFIT, Event_UPDATE_OUTFIT);
    }

    public void StopListenToEvents()
    {
        EventManager.RemoveListener(GameEvent.UPDATE_OUTFIT, Event_UPDATE_OUTFIT);
    }

    public void Click()
    {

    }

    public void Event_UPDATE_OUTFIT()
    {
        foreach (Transform child in tf_Content)
        {
            GameObject.Destroy(child.gameObject);
        }

        // _recyclableScrollRect.ReloadData();
        img_RarityBG.color = m_RarityBG[(int)m_OutfitRarity - 1];
        _recyclableScrollRect.Initialize();
        InitCell();
    }

    //Initialising _contactList with dummy data 
    private void InitCell()
    {
        if (_contactList != null) _contactList.Clear();

        // _dataLength = len;

        Dictionary<int, CharacterDataConfig> charConfig = GameData.Instance.GetCharacterDataConfig();
        int len = charConfig.Count;

        for (int i = 1; i <= len; i++)
        {
            if (charConfig[i].GetRatity() == (int)m_OutfitRarity)
            {
                UICharacterCardInfo obj = new UICharacterCardInfo();
                obj.m_Id = charConfig[i].m_Id;
                obj.m_Name = charConfig[i].m_Name;
                obj.m_Price = charConfig[i].m_Price.ToString();
                obj.m_AdsNumber = charConfig[i].m_AdsNumber;
                obj.m_Rarity = charConfig[i].m_IsRarity;
                _contactList.Add(obj);
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as UICharacterCard;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}

public enum OutfitRarity
{
    RARE = 1,
    EPIC = 2,
    LEGEND = 3,
}