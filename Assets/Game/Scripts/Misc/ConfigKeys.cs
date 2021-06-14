using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigKeys
{
    public static string m_Pipe1 = "Pipe1";
    public static string m_Pipe0 = "Pipe0";
    public static string m_ScoreLine = "Score";
    public static string m_GoldEffect1 = "GoldEffect";

    public static readonly int m_Idle = Animator.StringToHash("Idle");
    public static readonly int m_Run = Animator.StringToHash("Run");
    public static readonly int m_Hang = Animator.StringToHash("Hang");
    public static readonly int m_JumpDown = Animator.StringToHash("JumpDown");

    public static string m_PipeCollect = "PipeCollect";
    public static string m_PathAction = "PathAction";
    public static string m_Ending = "Ending";
    public static string m_KeyInGame = "KeyInGame";
}
