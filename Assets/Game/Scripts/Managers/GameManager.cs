using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;
using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public int m_ScoreFactor;
    public List<Color> m_ScoreLineColor;
    public int m_KeyInGameStep;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Vibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public void StopAllVibratesq()
    {
        MMVibrationManager.StopAllHaptics(true);
    }
}
