using UnityEngine;

public class JumpUpState : IState<Character>
{
    private static JumpUpState m_Instance;
    private JumpUpState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static JumpUpState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new JumpUpState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnJumpUpEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnJumpUpExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnJumpUpExit();
    }
}