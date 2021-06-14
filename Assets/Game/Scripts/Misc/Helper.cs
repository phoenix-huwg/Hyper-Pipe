using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Helper
{
    //DISTANCE
    public static float CalDistance(Vector2 _origin, Vector2 _des)
    {
        return Vector2.Distance(_origin, _des);
    }

    public static float CalDistance(Vector3 _origin, Vector3 _des)
    {
        return Vector3.Distance(_origin, _des);
    }

    // public static float CalDistanceXZ(Vector3 _origin, Vector3 _des)
    // {
    //     Vector3 origin = new Vector3();
    //     Vector3 des = _des;

    //     return Vector3.Distance(origin, des);
    // }

    public static bool InRange(Vector3 _origin, Vector3 _des, float _maxDistance)
    {
        return (Vector3.Distance(_origin, _des) <= _maxDistance);
    }

    public float CalDistance2(Vector2 origin, Vector2 des)
    {
        return (origin - des).magnitude;
    }

    public float CalDistance2(Vector3 origin, Vector3 des)
    {
        return Vector3.Distance(origin, des);
    }

    //ROTATION
    public static float ClampAngle(float angle, float min, float max)
    {
        if (min < 0 && max > 0 && (angle > max || angle < min))
        {
            angle -= 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        else if (min > 0 && (angle > max || angle < min))
        {
            angle += 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }

        if (angle < min) return min;
        else if (angle > max) return max;
        else return angle;
    }

    public static Quaternion Random8Direction(Vector3 _ownerPos)
    {
        // Vector3 dir = new Vector3();
        // int a = Random.Range(0, 8);
        // switch (a)
        // {
        //     case 0:
        //         dir = new Vector3(1, 0, 0);
        //         break;
        //     case 1:
        //         dir = new Vector3(-1, 0, 0);
        //         break;
        //     case 2:
        //         dir = new Vector3(0, 0, 1);
        //         break;
        //     case 3:
        //         dir = new Vector3(0, 0, -1);
        //         break;
        //     case 4:
        //         dir = new Vector3(1, 0, 1);
        //         break;
        //     case 5:
        //         dir = new Vector3(-1, 0, -1);
        //         break;
        //     case 6:
        //         dir = new Vector3(1, 0, -1);
        //         break;
        //     case 7:
        //         dir = new Vector3(-1, 0, 1);
        //         break;

        // }

        Vector3 dir = new Vector3();
        int a = Random.Range(0, 4);
        switch (a)
        {
            case 0:
                dir = Vector3.left;
                break;
            case 1:
                dir = Vector3.right;
                break;
            case 2:
                dir = Vector3.forward;
                break;
            case 3:
                dir = Vector3.back;
                break;
        }

        // dir = tf_Owner.position + new Vector3(5, 0, 0);

        Vector3 dir2 = _ownerPos + dir;

        Vector3 lookPos = dir2 - _ownerPos;

        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);

        return rotation;
    }

    #region DEBUG
    public static void DebugLog(string mess)
    {
        // #if PLATFORM_EDITOR
        Debug.Log(mess);
        // #endif
    }

    public static void DebugLog(float mess)
    {
        Debug.Log(mess);
    }

    public static void DebugLog(int mess)
    {
        Debug.Log(mess);
    }
    #endregion

    #region RANDOM
    public static bool Random2Probability(float percent)
    {
        float pickPercent = Random.Range(1, 101);

        if (pickPercent <= percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    public static bool JsonDataContainsKey(JsonData data, string key)
    {
        bool result = false;
        if (data == null)
            return result;
        if (!data.IsObject)
        {
            return result;
        }
        IDictionary tdictionary = data as IDictionary;
        if (tdictionary == null)
            return result;
        if (tdictionary.Contains(key))
        {
            result = true;
        }
        return result;
    }

    #region OBJECT_MOVING

    public static Vector3 Follow(Vector3 _target, Vector3 _owner, Vector3 _offset, float _smooth = 1)
    {
        Vector3 posOffset = _target + _offset;
        Vector3 posSmooth = Vector3.Lerp(_owner, posOffset, _smooth);

        return posSmooth;
    }

    public static Vector3 Follow(Vector3 _target, Vector3 _owner, Vector3 _offset)
    {
        return (_target + _offset);
    }

    #endregion

    #region OBJECT_ROTATING

    public static float AngleBetweenVectors(Vector2 v1, Vector2 v2)
    {
        Vector2 fromVector2 = v1;
        Vector2 toVector2 = v2;

        float ang = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
            ang = 360 - ang;

        return ang;
    }

    #endregion

    public static string ConvertSecondsToDateTimeFormat(int type, double defValue)
    {
        if (type == 4)
        {
            string ret;
            int min = (int)(defValue / 60);
            int sec = (int)(defValue % 60);
            int hour = min / 60;
            int day = hour / 24;
            min = min % 60;
            hour = hour % 24;
            ret = (day >= 10 ? day.ToString() : "0" + day.ToString()) + "D:" + (hour >= 10 ? hour.ToString() : "0" + hour.ToString()) + "H:" + (min >= 10 ? min.ToString() : "0" + min.ToString()) + "M:" + (sec >= 10 ? sec.ToString() : "0" + sec.ToString()) + "S";
            return ret;
        }
        else if (type == 3)
        {
            string ret;
            int min = (int)(defValue / 60);
            int sec = (int)(defValue % 60);
            int hour = min / 60;
            min = min % 60;
            ret = (hour >= 10 ? hour.ToString() : "0" + hour.ToString()) + ":" + (min >= 10 ? min.ToString() : "0" + min.ToString()) + ":" + (sec >= 10 ? sec.ToString() : "0" + sec.ToString());
            return ret;
        }
        else if (type == 2)
        {
            string ret;
            int min = (int)(defValue / 60);
            int sec = (int)(defValue % 60);
            ret = (min >= 10 ? min.ToString() : "0" + min.ToString()) + ":" + (sec >= 10 ? sec.ToString() : "0" + sec.ToString());
            return ret;
        }
        else
        {
            return null;
        }
    }
}
