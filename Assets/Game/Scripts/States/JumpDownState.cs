using UnityEngine;

public class JumpDownState : IState<Character>
{
    private static JumpDownState m_Instance;
    private JumpDownState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static JumpDownState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new JumpDownState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnJumpDownEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnJumpDownExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnJumpDownExit();
    }
}