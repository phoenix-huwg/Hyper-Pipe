﻿//
//SpringBone.cs for unity-chan!
//
//Original Script is here:
//ricopin / SpringBone.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Revised by N.Kobayashi 2014/06/20
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
    public class SpringBone : MonoBehaviour
    {
        //次のボーン
        public Transform child;
        public Pipe m_Pipe;

        //ボーンの向き
        public Vector3 boneAxis = new Vector3(-1.0f, 0.0f, 0.0f);
        public float radius = 0.05f;

        //各SpringBoneに設定されているstiffnessForceとdragForceを使用するか？
        public bool isUseEachBoneForceSettings = false;

        //バネが戻る力
        public float stiffnessForce = 0.01f;

        //力の減衰力
        public float dragForce = 0.4f;
        public Vector3 springForce = new Vector3(0.0f, -0.0001f, 0.0f);
        public SpringCollider[] colliders;
        public bool debug = true;
        //Kobayashi:Thredshold Starting to activate activeRatio
        public float threshold = 0.01f;
        public float springLength;
        public Quaternion localRotation;
        public Transform trs;
        public Vector3 currTipPos;
        public Vector3 prevTipPos;
        //Kobayashi
        public Transform org;
        //Kobayashi:Reference for "SpringManager" component with unitychan 
        public SpringManager managerRef;

        private void Awake()
        {
            trs = transform;
            localRotation = transform.localRotation;
            //Kobayashi:Reference for "SpringManager" component with unitychan
            // GameObject.Find("unitychan_dynamic").GetComponent<SpringManager>();
            // managerRef = GetParentSpringManager(transform);
        }

        private void OnEnable()
        {
            if (trs == null)
            {
                trs = transform;
            }

            StartListenToEvents();
        }

        private void OnDisable()
        {
            StopListenToEvents();
        }

        private void OnDestroy()
        {
            StopListenToEvents();
        }

        public void StartListenToEvents()
        {
            EventManager.AddListener(GameEvent.SET_PIPE_BONE_CHILD, SetPipeBoneChild);
        }

        public void StopListenToEvents()
        {
            EventManager.RemoveListener(GameEvent.SET_PIPE_BONE_CHILD, SetPipeBoneChild);
        }

        public void SetPipeBoneChild()
        {
            if (transform.childCount > 0)
            {
                child = transform.GetChild(0);
            }
            else
            {
                child = transform;
            }

            springLength = Vector3.Distance(trs.position, child.position);
        }

        private SpringManager GetParentSpringManager(Transform t)
        {
            var springManager = t.GetComponent<SpringManager>();

            if (springManager != null)
                return springManager;

            if (t.parent != null)
            {
                return GetParentSpringManager(t.parent);
            }

            return null;
        }

        public void SetParentSpringManager()
        {
            // managerRef = GetParentSpringManager(transform);
            managerRef = InGameObjectsManager.Instance.m_Char.m_SpringManager;
        }

        private void Start()
        {
            springLength = Vector3.Distance(trs.position, child.position);
            currTipPos = child.position;
            prevTipPos = child.position;
        }

        public void UpdateSpring()
        {
            //Kobayashi
            org = trs;
            //回転をリセット
            trs.localRotation = Quaternion.identity * localRotation;

            float sqrDt = Time.deltaTime * Time.deltaTime;

            //stiffness
            Vector3 force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

            //drag
            force += (prevTipPos - currTipPos) * dragForce / sqrDt;

            force += springForce / sqrDt;

            //前フレームと値が同じにならないように
            Vector3 temp = currTipPos;

            //verlet
            currTipPos = (currTipPos - prevTipPos) + currTipPos + (force * sqrDt);

            //長さを元に戻す
            currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;

            //衝突判定
            // for (int i = 0; i < colliders.Length; i++)
            // {
            //     if (Vector3.Distance(currTipPos, colliders[i].transform.position) <= (radius + colliders[i].radius))
            //     {
            //         Vector3 normal = (currTipPos - colliders[i].transform.position).normalized;
            //         currTipPos = colliders[i].transform.position + (normal * (radius + colliders[i].radius));
            //         currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;
            //     }


            // }

            prevTipPos = temp;

            //回転を適用；
            Vector3 aimVector = trs.TransformDirection(boneAxis);
            Quaternion aimRotation = Quaternion.FromToRotation(aimVector, currTipPos - trs.position);
            //original
            //trs.rotation = aimRotation * trs.rotation;
            //Kobayahsi:Lerp with mixWeight
            Quaternion secondaryRotation = aimRotation * trs.rotation;

            trs.rotation = Quaternion.Lerp(org.rotation, secondaryRotation, managerRef.dynamicRatio);
        }

        // private void OnDrawGizmos()
        // {
        //     if (debug)
        //     {
        //         Gizmos.color = Color.yellow;
        //         Gizmos.DrawWireSphere(currTipPos, radius);
        //     }
        // }
    }
}
