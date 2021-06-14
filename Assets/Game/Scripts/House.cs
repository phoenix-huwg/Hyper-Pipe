using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject[] g_Waters;
    public bool m_Start = false;

    public float m_Time;
    public float m_TimeMax;

    public bool m_OutOfFire = false;

    public void OnEnable()
    {
        m_Time = 0f;
        m_TimeMax = 0.5f;
    }

    private void Update()
    {
        if (m_Start)
        {
            if (InGameObjectsManager.Instance.m_Char.m_RotatePipe)
            {
                if (m_Time >= m_TimeMax)
                {
                    Offffff();
                }
                else
                {
                    m_Time += Time.deltaTime;
                }
            }
        }
    }

    public void Offffff()
    {
        bool aaa = false;

        for (int i = 0; i < g_Waters.Length; i++)
        {
            if (g_Waters[i].activeInHierarchy)
            {
                aaa = true;
                break;
            }
        }

        if (!aaa)
        {
            PopupCaller.OpenWinPopup();
            this.enabled = false;
            Time.timeScale = 0;
        }

        for (int i = 0; i < g_Waters.Length; i++)
        {
            if (g_Waters[i].activeInHierarchy)
            {
                g_Waters[i].SetActive(false);
                GameManager.Instance.Vibrate();
                break;
            }
        }

        m_Time = 0f;
    }
}
