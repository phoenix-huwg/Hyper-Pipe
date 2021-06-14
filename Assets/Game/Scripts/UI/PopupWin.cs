using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : UICanvas
{
    public Button btn_NextLevel;

    private void Awake()
    {
        m_ID = UIID.POPUP_WIN;
        Init();

        GUIManager.Instance.AddClickEvent(btn_NextLevel, OnNextLevel);


        // SetChar(ProfileManager.GetSelectedCharacter());
    }

    public void OnNextLevel()
    {
        Time.timeScale = 1;
        GUIManager.Instance.LoadPlayScene();
    }
}
