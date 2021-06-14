using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityChan;

public class Pipe : InGameObject
{
    public int m_PipeNo;
    private Collider m_Col;
    private bool m_Collided = false;
    public SpringBone m_SpringBone;

    private void Awake()
    {
        m_Col = GetComponent<Collider>();
        tf_Owner = GetComponent<Transform>();
    }

    public override void OnEnable()
    {
        m_Collided = false;

        if (m_Col != null)
        {
            m_Col.isTrigger = true;
        }

        base.OnEnable();
    }

    public void TestPos()
    {
        if (m_PipeNo != 0 && m_PipeNo != 1)
        {
            tf_Owner.transform.localPosition = new Vector3(0.1f, 468f, 0f);
        }
    }

    public void Setup()
    {
        Character charrr = InGameObjectsManager.Instance.m_Char;
        int pipeCount = charrr.m_SpringManager.springBones.Count;
        m_PipeNo = pipeCount;
        charrr.m_SpringManager.springBones.Add(m_SpringBone);
        EventManager.CallEvent(GameEvent.SET_PIPE_BONE_CHILD);
        // charrr.m_SpringManager.springBones.Add(m_SpringBone);
    }

    public override void StartListenToEvents()
    {
        // EventManager.AddListener(GameEvent.SET_PIPE_BONE_CHILD, SetPipeBoneChild);
        EventManager.AddListener(GameEvent.TEST_POS, TestPos);
    }

    public override void StopListenToEvents()
    {
        // EventManager.RemoveListener(GameEvent.SET_PIPE_BONE_CHILD, SetPipeBoneChild);
        EventManager.RemoveListener(GameEvent.TEST_POS, TestPos);
    }

    // public void SetPipeBoneChild()
    // {
    //     Character charrr = InGameObjectsManager.Instance.m_Char;
    //     int pipeCount = charrr.m_SpringManager.springBones.Count;
    //     // if (m_PipeNo == pipeCount)
    //     // if (m_PipeNo == (pipeCount - 1))
    //     // // if (pipeCount == 0)
    //     // {
    //     //     m_SpringBone.child = m_SpringBone.transform;
    //     // }
    //     // else
    //     // {
    //     //     // m_SpringBone.child = charrr.m_SpringManager.springBones[m_PipeNo - 1].transform;
    //     //     // tf_Owner.
    //     //     m_SpringBone.child = charrr.m_SpringManager.springBones[pipeCount - m_PipeNo - 1].transform;
    //     // }
    //     // Transform child = tf_Owner.GetChild(0);
    //     // if (child == null)
    //     // {
    //     //     m_SpringBone.child = tf_Owner;
    //     // }
    //     // else
    //     // {
    //     //     m_SpringBone.child = child;
    //     // }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Cutter"))
        {
            if (!m_Collided && m_PipeNo != 0)
            {
                m_Collided = true;
                if (GetComponent<Rigidbody>() == null)
                {
                    Rigidbody sc = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
                }
                transform.parent = null;
                m_Col.isTrigger = false;
                EventManager1<int>.CallEvent(GameEvent.CUT_PIPE, m_PipeNo);

                GameManager.Instance.Vibrate();

                IEnemyBlockable iEB = other.gameObject.GetComponent<IEnemyBlockable>();

                if (iEB != null)
                {
                    iEB.BlockEnemy();
                }

                StartCoroutine(DeathAction());
            }
        }
    }

    public void DetachAction()
    {
        m_Collided = true;
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody sc = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        m_Col.isTrigger = false;

        StartCoroutine(DeathAction());
    }

    IEnumerator DeathAction()
    {
        yield return new WaitForSeconds(2f);
        Destroy(GetComponent<Rigidbody>());
        PrefabManager.Instance.DespawnPool(this.gameObject);
    }
}
