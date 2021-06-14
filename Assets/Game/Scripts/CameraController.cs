using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraController : Singleton<CameraController>
{
    public Transform tf_Owner;
    public CinemachineFreeLook m_CMFreeLook;
    public CinemachineCameraOffset m_CMOffset;
    public GameObject g_Wind;

    [Header("Lerp cam follow pos")]
    float timeElapsed;
    float lerpDuration = 3;

    float startValue = 2.23f;
    float endValue = 0f;
    float valueToLerp;

    private void OnEnable()
    {
        timeElapsed = 4f;
    }

    private void Update()
    {
        // if (timeElapsed < lerpDuration)
        // {
        //     valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
        //     m_CMOffset.m_Offset.y = valueToLerp;
        //     timeElapsed += Time.deltaTime;
        // }
    }

    public void DoActionByPath()
    {
        tf_Owner.DORotate(new Vector3(7f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed = 0f;
        StartCoroutine(ChangeCMOffset(true));
    }

    public void UndoActionByPath()
    {
        tf_Owner.DORotate(new Vector3(19f, 0f, 0f), 1.5f, RotateMode.Fast);
        timeElapsed = 0f;
        StartCoroutine(ChangeCMOffset(false));
    }

    IEnumerator ChangeCMOffset(bool _hanging)
    {
        while (timeElapsed < lerpDuration)
        {
            if (_hanging)
            {
                valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            }
            else
            {
                valueToLerp = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
            }
            m_CMOffset.m_Offset.y = valueToLerp;
            timeElapsed += Time.deltaTime;
        }
        yield return new WaitUntil(() => timeElapsed >= lerpDuration);
    }

    public void TestCinematic()
    {
        tf_Owner.DORotate(new Vector3(19f, 178.81f, 0f), 1.5f);
    }
}
