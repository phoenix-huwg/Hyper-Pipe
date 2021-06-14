using UnityEngine;

public class HangingState : IState<Character>
{
    private static HangingState m_Instance;
    private HangingState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static HangingState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new HangingState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnHangingEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnHangingExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnHangingExit();
    }
}