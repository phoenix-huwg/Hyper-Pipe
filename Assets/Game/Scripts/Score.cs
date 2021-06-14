using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Score : InGameObject
{
    public static int m_Score;
    public int m_ScoreLine;
    public TextMeshProUGUI txt_Score;
    public Image img_BG;

    public override void StartListenToEvents()
    {
        // EventManager.AddListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager1<int>.AddListener(GameEvent.DESTROY_SCORE_LINE, Event_SCORE_LINE_PICK);
    }

    public override void StopListenToEvents()
    {
        // EventManager.RemoveListener(GameEvent.END_GAME, Event_END_GAME);
        EventManager1<int>.RemoveListener(GameEvent.DESTROY_SCORE_LINE, Event_SCORE_LINE_PICK);
    }

    // public void Event_SCORE_LINE_PICK(bool _logic)
    // {
    //     if (_logic)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public void Event_SCORE_LINE_PICK(int _result)
    {
        Character charrr = InGameObjectsManager.Instance.m_Char;

        if (m_ScoreLine == _result)
        {
            Helper.DebugLog("m_ScoreLine: " + m_ScoreLine + " Get picked!!!!!!");
            InGameObjectsManager.Instance.m_House = PrefabManager.Instance.SpawnHouse(tf_Owner.position.z + 25f).GetComponent<House>();
            charrr.tf_Owner.DOMove(tf_Owner.position, 2f).OnComplete
            (
                () =>
                {
                    charrr.ChangeState(IdleState.Instance);
                    charrr.m_LastAction = true;
                    EventManager1<bool>.CallEvent(GameEvent.WATER, true);
                    InGameObjectsManager.Instance.m_House.m_Start = true;
                }
            );
        }
        // else
        // {
        //     Helper.DebugLog("m_ScoreLine: " + m_ScoreLine + " Get destroyed!!!!!!");
        //     Destroy(gameObject);
        // }

        // if (m_ScoreLine != _result)
        // {
        //     Helper.DebugLog("Score line destroy is: " + m_ScoreLine);
        //     PrefabManager.Instance.DespawnPool(this.gameObject);
        // }
        // Helper.DebugLog("Score line destroy is: " + m_ScoreLine);
        // Destroy(gameObject);
    }


    public void SetScore(int _scoreLine, int _value, Color _color)
    {
        m_Score++;
        // m_ScoreLine = _scoreLine;
        m_ScoreLine = m_Score;
        txt_Score.text = "x" + _value.ToString();
        img_BG.color = new Color(_color.r, _color.g, _color.b);
    }

    public void Blink()
    {
        float r = img_BG.color.r;
        float g = img_BG.color.g;
        float b = img_BG.color.b;
        img_BG.DOColor(Color.white, 0.3f).OnComplete
        (
            () => img_BG.DOColor(new Color(r, g, b), 0.5f)
        );
    }

    private void Update()
    {
        // img_BG.DOColor();
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     // img_BG.color = GameManager.Instance.m_Gradients[0].colorKeys;
        //     float r = img_BG.color.r;
        //     float g = img_BG.color.g;
        //     float b = img_BG.color.b;
        //     // img_BG.DOColor(Color.white, 0.3f).OnComplete
        //     // (
        //     //     () => img_BG.DOColor(new Color(r, g, b), 0.5f)
        //     // );
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        Blink();
    }
}