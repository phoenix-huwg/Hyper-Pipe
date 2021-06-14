using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInGame : MonoBehaviour
{
    public Button btn_LoadMapTest;
    public Button btn_AddPipeTest;
    // public Button btn_Outfit;


    private void Awake()
    {
        // GUIManager.Instance.AddClickEvent(btn_LoadMapTest, LoadMapTest);
        // GUIManager.Instance.AddClickEvent(btn_AddPipeTest, AddPipeTest);

        // GUIManager.Instance.AddClickEvent(btn_Outfit, OpenOutfitPopup);
    }

    public void OnEnable()
    {
        // btn_Outfit.gameObject.SetActive(false);
    }

    // private void OnDisable()
    // {

    // }

    // private void OnDestroy()
    // {

    // }

    // public void StartListenToEvents()
    // {

    // }

    // public void StartListenToEvents()
    // {

    // }

    public void Event_GAME_START(bool _value)
    {
        // btn_Outfit.gameObject.SetActive(_value);
    }

    // public void LoadMapTest()
    // {
    //     GUIManager.Instance.LoadPlayScene();
    // }

    public void AddPipeTest()
    {
        InGameObjectsManager.Instance.m_Char.AddPipe();
    }

    public void OpenOutfitPopup()
    {
        PopupCaller.OpenOutfitPopup();
    }
}
