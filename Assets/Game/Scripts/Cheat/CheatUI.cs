using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatUI : MonoBehaviour
{
    public Button btn_Gold;
    public TMP_InputField m_InputGold;

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_Gold, AddGold);
        gameObject.SetActive(false);
    }

    public void AddGold()
    {
        BigNumber gold = new BigNumber(m_InputGold.text);
        ProfileManager.SetGold(gold);
        PlaySceneManager.Instance.txt_TotalGold.text = ProfileManager.GetGold();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
