using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;
using UnityEngine.UI;
using TMPro;

public class PlaySceneManager : Singleton<PlaySceneManager>
{
    public GameObject g_Map;
    public TouchTrackPad m_TouchTrackPad;
    public GameObject g_JoystickTrackPad;
    public TouchJoystick m_JoystickTrackPad;

    public Button btn_LoadMapTest;
    public Button btn_AddPipeTest;
    public Button btn_Outfit;

    public Button btn_TestAds;

    public TextMeshProUGUI txt_TotalGold;
    public TextMeshProUGUI txt_Level;

    public GameObject g_Hand;

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_Outfit, OpenOutfitPopup);
        // GUIManager.Instance.AddClickEvent(btn_TestAds, TestAds);
    }

    private void OnEnable()
    {
        txt_TotalGold.text = ProfileManager.GetGold();
        txt_Level.text = "Level " + ProfileManager.GetLevel().ToString();
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

    public override void StartListenToEvents()
    {
        EventManager.AddListener(GameEvent.LOAD_MAP, Event_LOAD_MAP);
        EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager.AddListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        EventManager1<bool>.AddListener(GameEvent.GAME_START, Event_GAME_START);
    }

    public override void StopListenToEvents()
    {
        EventManager.RemoveListener(GameEvent.LOAD_MAP, Event_LOAD_MAP);
        EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager.RemoveListener(GameEvent.UPDATE_GOLD, Event_UPDATE_GOLD);
        EventManager1<bool>.AddListener(GameEvent.GAME_START, Event_GAME_START);
    }

    public void Event_UPDATE_GOLD()
    {
        txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void Event_LOAD_MAP()
    {
        m_TouchTrackPad.gameObject.SetActive(true);
        btn_Outfit.gameObject.SetActive(false);
    }

    public void Event_END_GAME()
    {
        m_TouchTrackPad.gameObject.SetActive(false);
        Helper.DebugLog("PlaySceneManager end game!!!!");
    }

    public void Event_GAME_START(bool _value)
    {
        btn_Outfit.gameObject.SetActive(_value);
    }

    public void OpenOutfitPopup()
    {
        PopupCaller.OpenOutfitPopup();
    }

    public void TestAds()
    {
        // AdsManager.Instance.WatchInterstitial();
    }
}
