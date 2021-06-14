using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatGame : MonoBehaviour
{
    public GameObject g_CheatUI;
    public Button btn_CheatUI;

    private void Awake()
    {
        // g_CheatUI.SetActive(true);
        GUIManager.Instance.AddClickEvent(btn_CheatUI, OpenCheatUI);
    }

    public void OpenCheatUI()
    {
        g_CheatUI.SetActive(!g_CheatUI.activeInHierarchy);
    }
}
