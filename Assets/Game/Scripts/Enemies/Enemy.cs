using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : InGameObject
{
    public Transform tf_SawModel;
    public Transform tf_Saw;
    public Transform tf_Start;
    public Transform tf_End;

    [Header("Speed")]
    public float m_MoveDownSpd;
    public float m_MoveUpSpd;

    [Header("Action Time")]
    public float m_ActionWaiting;
    protected float m_ActionTime;
    protected float m_ActionTimeMax;

    [Header("Action Delay Before Start Time")]
    public float m_StartDelayTimeMax;
    protected float m_StartDelayTime;

    public override void OnEnable()
    {
        m_ActionTimeMax = m_MoveDownSpd + m_MoveUpSpd + m_ActionWaiting;
        m_ActionTime = m_ActionTimeMax;

        m_StartDelayTime = 0f;

        base.OnEnable();
    }

    public virtual void Update()
    {
        if (m_StartDelayTime < m_StartDelayTimeMax)
        {
            m_StartDelayTime += Time.deltaTime;
        }
        else
        {
            if (m_ActionTime >= m_ActionTimeMax)
            {
                m_ActionTime = 0f;
                StartAction();
            }
            else
            {
                m_ActionTime += Time.deltaTime;
            }
        }
    }

    public virtual void StartAction()
    {
        tf_Saw.DOLocalMove(tf_End.localPosition, m_MoveDownSpd).OnComplete(
            () => tf_Saw.DOLocalMove(tf_Start.localPosition, m_MoveUpSpd)
        );
    }
}

public interface IEnemyBlockable
{
    void BlockEnemy();
}