//
//SpingManager.cs for unity-chan!
//
//Original Script is here:
//ricopin / SpingManager.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Revised by N.Kobayashi 2014/06/24
//           Y.Ebata
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityChan
{
    public class SpringManager : MonoBehaviour
    {
        //Kobayashi
        // DynamicRatio is paramater for activated level of dynamic animation 
        public float dynamicRatio = 1.0f;

        //Ebata
        public float stiffnessForce;
        public AnimationCurve stiffnessCurve;
        public float dragForce;
        public AnimationCurve dragCurve;
        public List<SpringBone> springBones;

        // void Start()
        // {
        //     UpdateParameters();
        // }

        // #if UNITY_EDITOR
        //         void Update()
        //         {

        //             //Kobayashi
        //             if (dynamicRatio >= 1.0f)
        //                 dynamicRatio = 1.0f;
        //             else if (dynamicRatio <= 0.0f)
        //                 dynamicRatio = 0.0f;
        //             //Ebata
        //             UpdateParameters();

        //         }
        // #endif
        // private void LateUpdate()
        // {
        //     //Kobayashi
        //     if (dynamicRatio != 0.0f)
        //     {
        //         for (int i = 0; i < springBones.Count; i++)
        //         {
        //             if (dynamicRatio > springBones[i].threshold)
        //             {
        //                 springBones[i].UpdateSpring();
        //             }
        //         }
        //     }
        // }

        private void Update()
        {
            //Kobayashi
            if (springBones.Count > 0)
            {
                if (dynamicRatio != 0.0f)
                {
                    for (int i = 0; i < springBones.Count; i++)
                    {
                        if (dynamicRatio > springBones[i].threshold)
                        {
                            springBones[i].UpdateSpring();
                        }
                    }
                }
            }
        }

        //         private void UpdateParameters()
        //         {
        //             UpdateParameter("stiffnessForce", stiffnessForce, stiffnessCurve);
        //             UpdateParameter("dragForce", dragForce, dragCurve);
        //         }

        //         private void UpdateParameter(string fieldName, float baseValue, AnimationCurve curve)
        //         {
        // #if UNITY_EDITOR
        //             var start = curve.keys[0].time;
        //             var end = curve.keys[curve.length - 1].time;
        //             //var step	= (end - start) / (springBones.Length - 1);

        //             var prop = springBones[0].GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        //             for (int i = 0; i < springBones.Count; i++)
        //             {
        //                 //Kobayashi
        //                 if (!springBones[i].isUseEachBoneForceSettings)
        //                 {
        //                     var scale = curve.Evaluate(start + (end - start) * i / (springBones.Count - 1));
        //                     prop.SetValue(springBones[i], baseValue * scale);
        //                 }
        //             }
        // #endif
        //         }

        public void RemoveObject(int _value)
        {
            for (int i = _value; i < springBones.Count; i++)
            {
                if (springBones[i].transform.parent != null)
                {
                    springBones[i].transform.parent = null;
                    springBones[i].m_Pipe.DetachAction();
                    // springBones.Remove(springBones[i]);

                    if (springBones[i].child.transform.parent != null)
                    {
                        springBones[i].child.transform.parent = null;
                        springBones[i].child.GetComponent<SpringBone>().m_Pipe.DetachAction();
                        // springBones.Remove(springBones[i].child.GetCompo nent<SpringBone>());
                    }
                }

                // springBones.RemoveAt(i);
            }

            while (springBones.Count > _value)
            {
                int i = springBones.Count - 1;

                springBones.RemoveAt(i);
            }

            // for (int i = _value; i < springBones.Count; i++)
            // {
            //     springBones.RemoveAt(i);
            //     Helper.DebugLog("BBBBBBBBBBBBBBBBBBBBBB");
            // }
        }
    }
}