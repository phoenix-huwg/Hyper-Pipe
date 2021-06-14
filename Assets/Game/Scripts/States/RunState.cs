using UnityEngine;

public class RunState : IState<Character>
{
    private static RunState m_Instance;
    private RunState()
    {
        if (m_Instance != null)
        {
            return;
        }

        m_Instance = this;
    }
    public static RunState Instance
    {
        get
        {
            if (m_Instance == null)
            {
                new RunState();
            }

            return m_Instance;
        }
    }

    public void Enter(Character _charState)
    {
        _charState.OnRunEnter();
    }

    public void Execute(Character _charState)
    {
        _charState.OnRunExecute();
    }

    public void Exit(Character _charState)
    {
        _charState.OnRunExit();
    }
}