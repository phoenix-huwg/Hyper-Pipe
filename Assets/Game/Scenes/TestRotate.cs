using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class TestRotate : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform tf_Target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            agent.Move(tf_Target.position);
        }
    }
}
