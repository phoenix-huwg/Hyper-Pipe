using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityChan;
using ControlFreak2;
using DG.Tweening;

public class Character : InGameObject
{
    public CharacterType m_CharacterType;
    public EState m_EState;
    public CharacterController cc_Owner;
    public SpringManager m_SpringManager;
    public Animator anim_Owner;
    public StateMachine<Character> m_StateMachine;
    public Transform tf_PipeHolder;

    public PathAction m_PathAction;
    public bool m_ActiveJoystick;

    public bool m_ReachEnding;

    public float m_SpdFactor;

    public bool m_LastAction = false;
    public bool m_RotatePipe = false;

    public bool m_OnAir = false;

    // [Header("Action Time")]
    // public float

    public override void StartListenToEvents()
    {
        EventManager1<int>.AddListener(GameEvent.CUT_PIPE, RemovePipe);
        EventManager1<CharacterType>.AddListener(GameEvent.CHAR_DESTROY, DestroyChar);
        EventManager.AddListener(GameEvent.LOAD_MAP, DestroyChar);
    }

    public override void StopListenToEvents()
    {
        EventManager1<int>.RemoveListener(GameEvent.CUT_PIPE, RemovePipe);
        EventManager1<CharacterType>.RemoveListener(GameEvent.CHAR_DESTROY, DestroyChar);
        EventManager.RemoveListener(GameEvent.LOAD_MAP, DestroyChar);
    }

    public void DestroyChar()
    {
        Destroy(gameObject);
        Helper.DebugLog("Destroy Char");
    }

    public void DestroyChar(CharacterType _characterType)
    {
        if (_characterType == m_CharacterType)
        {
            Destroy(gameObject);
        }
        Helper.DebugLog("Destroy Char");
    }

    public void RemovePipe(int _value)
    {
        m_SpringManager.RemoveObject(_value);
    }

    public override void OnEnable()
    {
        m_LastAction = false;
        m_RotatePipe = false;
        m_StateMachine = new StateMachine<Character>(this);
        m_StateMachine.Init(IdleState.Instance);
        CameraController.Instance.m_CMFreeLook.Follow = tf_Owner;
        m_ActiveJoystick = true;
        m_ReachEnding = false;
        m_OnAir = false;

        StartCoroutine(AddFirstPipe());

        base.OnEnable();
    }

    private void Update()
    {
        // if (tf_Owner.position.y < -0.5f)
        // {
        //     GUIManager.Instance.LoadPlayScene();
        //     // CameraController.Instance.m_CMFreeLook.Follow = null;
        // }

        m_StateMachine.ExecuteStateUpdate();

        if (m_LastAction)
        {
            PlaySceneManager.Instance.g_JoystickTrackPad.SetActive(true);
            PlaySceneManager.Instance.g_Hand.SetActive(true);

            float angle = CF2Input.GetAxis("Joystick Move X") * 45f;
            angle = Mathf.Clamp(angle, -45f, 45f);
            tf_Owner.DORotate(new Vector3(0f, angle, 0f), 1.5f, RotateMode.Fast);

            if (PlaySceneManager.Instance.m_JoystickTrackPad.Pressed())
            {
                m_RotatePipe = true;
                if (PlaySceneManager.Instance.g_Hand.activeInHierarchy)
                {
                    PlaySceneManager.Instance.g_Hand.SetActive(false);
                }
            }
            else
            {
                m_RotatePipe = false;
            }
        }

        // if (tf_Owner.position.x >= 5.5f || tf_Owner.position.x <= -5.5f)
        // {
        //     cc_Owner.enabled = false;
        //     Helper.DebugLog("BOderrrrrrrrrrrrrrrr");
        // }

        // if (!cc_Owner.isGrounded)
        // if (CameraController.Instance.m_CMFreeLook.Follow == null)
        // {
        //     // tf_Owner.position = new Vector3(0f, Physics.gravity.y / 10f, 0f);

        //     // Vector3 aaa = new Vector3(0f, Physics.gravity.y / 100f, 0f);
        //     // Vector3 aaa = new Vector3(0f, -0.05f, 0f);
        //     // cc_Owner.Move(aaa.normalized);
        //     Helper.DebugLog("Char is not grounded");
        // }

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     AddPipe();
        // }

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     EventManager.CallEvent(GameEvent.TEST_POS);
        // }
    }

    IEnumerator AddFirstPipe()
    {
        yield return new WaitUntil(() => m_SpringManager != null);
        AddPipe();
    }

    public void AddPipe()
    {
        int pipeCount = m_SpringManager.springBones.Count;
        if (pipeCount > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                pipeCount = m_SpringManager.springBones.Count;
                List<SpringBone> bones = m_SpringManager.springBones;
                Pipe pipe = PrefabManager.Instance.SpawnPipePool(ConfigKeys.m_Pipe1, Vector3.zero).GetComponent<Pipe>();
                pipe.m_SpringBone.SetParentSpringManager();

                Transform tf_TargetPipe = m_SpringManager.springBones[pipeCount - 1].transform;
                if (pipeCount == 1)
                {
                    pipe.tf_Owner.parent = tf_PipeHolder;
                    // pipe.tf_Owner.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    pipe.tf_Owner.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
                    // pipe.tf_Owner.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    if (m_EState != EState.HANGING && m_EState != EState.JUMP_DOWN && m_EState != EState.JUMP_UP)
                    {
                        pipe.tf_Owner.localPosition = new Vector3(0f, tf_TargetPipe.position.y + 1.1f, 0f);
                    }
                    else
                    {
                        pipe.tf_Owner.localPosition = new Vector3(0f, 0.8899996f, 0f);
                        // pipe.tf_Owner.localPosition = new Vector3(0f, 0.5f, 0f);
                    }
                    // pipe.tf_Owner.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                }
                else
                {
                    pipe.tf_Owner.parent = tf_TargetPipe;
                    // pipe.tf_Owner.transform.localScale = new Vector3(1f, 1f, 1f);
                    pipe.tf_Owner.transform.localScale = new Vector3(1f, 1f, 1f);
                    if (m_EState != EState.HANGING && m_EState != EState.JUMP_DOWN && m_EState != EState.JUMP_UP)
                    {
                        pipe.tf_Owner.localPosition = new Vector3(0f, tf_TargetPipe.position.y - 0.5f, 0f);
                    }
                    else
                    {
                        pipe.tf_Owner.localPosition = new Vector3(0f, 0.8899996f, 0f);
                        // pipe.tf_Owner.localPosition = new Vector3(0f, 0.5f, 0f);
                    }
                    // pipe.tf_Owner.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                }
                pipe.tf_Owner.localEulerAngles = new Vector3(0f, 0f, 90f);

                pipe.Setup();

                GameObject go = PrefabManager.Instance.SpawnScoreLine(ConfigKeys.m_ScoreLine, new Vector3(0f, 0f, InGameObjectsManager.Instance.g_Ending.transform.position.z + Score.m_Score * 2 * 2 + 4f + Score.m_Score * 2));
                go.GetComponent<Score>().SetScore(InGameObjectsManager.Instance.m_ScoreLine, Score.m_Score, GameManager.Instance.m_ScoreLineColor[Score.m_Score % 7]);
                InGameObjectsManager.Instance.m_ScoreLine++;
            }
        }
        else
        {
            pipeCount = m_SpringManager.springBones.Count;
            List<SpringBone> bones = m_SpringManager.springBones;
            Pipe pipe = PrefabManager.Instance.SpawnPipePool(ConfigKeys.m_Pipe0, Vector3.zero).GetComponent<Pipe>();
            pipe.m_SpringBone.SetParentSpringManager();

            pipe.tf_Owner.parent = tf_PipeHolder;
            pipe.tf_Owner.localPosition = Vector3.zero;
            pipe.tf_Owner.localEulerAngles = new Vector3(0f, 0f, 0f);
            pipe.m_SpringBone.localRotation = Quaternion.Euler(270f, 180f, 0f);
            // pipe.tf_Owner.transform.localScale = new Vector3(0.2f, 1f, 0.2f);
            pipe.tf_Owner.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            pipe.Setup();
        }

        EventManager.CallEvent(GameEvent.TEST_POS);
    }

    public void OnRunEnter()
    {
        m_EState = EState.RUN;
        anim_Owner.SetTrigger(ConfigKeys.m_Run);
        // EventManager1<bool>.CallEvent(GameEvent.GAME_START, false);
        PlaySceneManager.Instance.btn_Outfit.gameObject.SetActive(false);
        PlaySceneManager.Instance.txt_Level.gameObject.SetActive(false);
    }

    public void OnRunExecute()
    {
        if (m_ReachEnding)
        {
            return;
        }

        if (!PlaySceneManager.Instance.m_TouchTrackPad.Pressed())
        {
            ChangeState(IdleState.Instance);
        }

        Vector3 moveInput = new Vector3();

        moveInput = new Vector3(CF2Input.GetAxis("Mouse X") * 2.5f, Physics.gravity.y, 1.7f);


        moveInput = moveInput.normalized;

        float gravity = 0f;

        gravity -= 9.81f * Time.deltaTime;
        cc_Owner.Move(new Vector3(moveInput.x, gravity, moveInput.z * 2f));
        if (cc_Owner.isGrounded) gravity = 0;
    }

    public void OnRunExit()
    {

    }

    public void OnIdleEnter()
    {
        m_EState = EState.IDLE;
        anim_Owner.SetTrigger(ConfigKeys.m_Idle);
        cc_Owner.Move(Vector3.zero);
    }

    public void OnIdleExecute()
    {
        if (PlaySceneManager.Instance.m_TouchTrackPad.Pressed() && !m_OnAir)
        {
            ChangeState(RunState.Instance);
        }
    }

    public void OnIdleExit()
    {

    }

    public void OnJumpUpEnter()
    {
        m_EState = EState.JUMP_UP;
        anim_Owner.SetTrigger(ConfigKeys.m_Hang);
        m_OnAir = true;
        tf_Owner.DOMove(m_PathAction.tf_HangingPoint.position, 1f).OnComplete(
            () =>
            {
                CameraController.Instance.DoActionByPath();
                tf_Owner.parent = m_PathAction.tf_HangingPoint;
                ChangeState(HangingState.Instance);
                m_PathAction.DoAction();
            }
        );
    }

    public void OnJumpUpExecute()
    {

    }

    public void OnJumpUpExit()
    {

    }

    public void OnJumpDownEnter()
    {
        m_EState = EState.JUMP_DOWN;
        anim_Owner.SetTrigger(ConfigKeys.m_JumpDown);
        tf_Owner.DORotate(new Vector3(0f, 0f, 0f), 0f);
        tf_Owner.parent = null;
        tf_Owner.DOMoveY(0f, 1f).OnStart(
            () =>
            {
                // PlaySceneManager.Instance.m_TouchTrackPad.gameObject.SetActive(false);
                anim_Owner.SetTrigger(ConfigKeys.m_Idle);
            }
        ).OnComplete(
            () =>
            {
                m_OnAir = false;
                // PlaySceneManager.Instance.m_TouchTrackPad.gameObject.SetActive(true);
                ChangeState(IdleState.Instance);
            }
        );
    }

    public void OnJumpDownExecute()
    {
        // ChangeState(RunState.Instance);
    }

    public void OnJumpDownExit()
    {
        m_OnAir = false;
    }

    public void OnHangingEnter()
    {
        m_EState = EState.HANGING;
    }

    public void OnHangingExecute()
    {

    }

    public void OnHangingExit()
    {

    }

    public void ChangeState(IState<Character> _state)
    {
        m_StateMachine.ChangeState(_state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(ConfigKeys.m_PipeCollect))
        {
            other.GetComponent<PipeCollect>().SetupCollected();
            AddPipe();
            GameManager.Instance.Vibrate();
            // Helper.DebugLog("Char trigger pipecollect");
        }
        else if (other.tag.Equals(ConfigKeys.m_PathAction))
        {
            m_PathAction = other.GetComponent<PathAction>();
            m_PathAction.col_Owner.enabled = false;
            ChangeState(JumpUpState.Instance);
        }
        else if (other.tag.Equals(ConfigKeys.m_Ending))
        {
            CameraController.Instance.g_Wind.SetActive(true);
            m_ReachEnding = true;
            other.enabled = false;
            ChangeState(RunState.Instance);
            EventManager.CallEvent(GameEvent.END_GAME);

            int value = m_SpringManager.springBones.Count - 1;
            int result = value / GameManager.Instance.m_ScoreFactor;

            // Helper.DebugLog("Value is: " + value);
            // Helper.DebugLog("m_ScoreFactor is: " + GameManager.Instance.m_ScoreFactor);
            // Helper.DebugLog("Result is: " + result);

            EventManager1<int>.CallEvent(GameEvent.DESTROY_SCORE_LINE, result);
            // Helper.DebugLog("Ending calledddddddddddddddddddd");
        }
        else if (other.tag.Equals(ConfigKeys.m_KeyInGame))
        {
            ProfileManager.Instance.AddKeys(1);
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("DeadPlane"))
        {
            // Helper.DebugLog();
            GUIManager.Instance.LoadPlayScene();
        }
    }
}

public enum EState
{
    IDLE,
    RUN,
    JUMP_UP,
    HANGING,
    JUMP_DOWN,
}
