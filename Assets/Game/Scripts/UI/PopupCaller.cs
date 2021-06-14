using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCaller : MonoBehaviour
{
    public static void OpenOutfitPopup()
    {
        PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;

        GUIManager.Instance.ShowUIPopup(popup);
    }

    public static void OpenWinPopup()
    {
        PopupWin popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_WIN) as PopupWin;

        GUIManager.Instance.ShowUIPopup(popup);
    }
}
