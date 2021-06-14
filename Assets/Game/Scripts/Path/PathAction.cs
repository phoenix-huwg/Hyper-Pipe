using UnityEngine;
using DG.Tweening;
using ControlFreak2;

public class PathAction : PathCell
{
    public Transform tf_PivotPoint;
    public Transform tf_EndPoint;
    public Transform tf_HangingPoint;
    public Collider col_Owner;
    public bool m_StartAction;

    public override void OnEnable()
    {
        m_StartAction = false;
        col_Owner.enabled = true;

        base.OnEnable();
    }

    private void Update()
    {
        if (m_StartAction)
        {
            float angle = CF2Input.GetAxis("Joystick Move X") * 45f;
            angle = Mathf.Clamp(angle, -45f, 45f);
            tf_PivotPoint.DORotate(new Vector3(0f, 0f, angle), 1.5f, RotateMode.Fast);
        }
    }

    public void DoAction()
    {
        m_StartAction = true;

        // GameManager.Instance.m_TouchTrackPad.gameObject.SetActive(false);
        PlaySceneManager.Instance.g_JoystickTrackPad.SetActive(true);

        tf_PivotPoint.DOLocalMove(tf_EndPoint.localPosition, 6f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                // GameManager.Instance.m_TouchTrackPad.gameObject.SetActive(true);
                PlaySceneManager.Instance.g_JoystickTrackPad.SetActive(false);
                m_StartAction = false;
                InGameObjectsManager.Instance.m_Char.ChangeState(JumpDownState.Instance);
                CameraController.Instance.UndoActionByPath();
                // InGameObjectsManager.Instance.m_Char.ChangeState(RunState.Instance);
                // InGameObjectsManager.Instance.m_Char.ChangeState(IdleState.Instance);
            }
        );
    }
}
