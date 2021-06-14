using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawHorizontal : Enemy
{
    // public Transform tf_SawModel;
    public bool start;

    public override void OnEnable()
    {
        start = true;
        base.OnEnable();
    }

    public override void Update()
    {
        tf_SawModel.Rotate(Vector3.up * (450f * Time.deltaTime), 1f);
        base.Update();
    }

    public override void StartAction()
    {
        if (start)
        {
            tf_Saw.DOLocalMove(tf_End.localPosition, m_MoveDownSpd).OnComplete(
                () => start = false
            );
        }
        else
        {
            tf_Saw.DOLocalMove(tf_Start.localPosition, m_MoveUpSpd).OnComplete(
                () => start = true
            );
        }
    }
}
