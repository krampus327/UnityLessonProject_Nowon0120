using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public State state;
    public PlayerState playerState;
    [HideInInspector] public PlayerStateMachineManager manager;
    [HideInInspector] public PlayerAnimator playerAnimator;

    public virtual void Awake()
    {
        manager = GetComponent<PlayerStateMachineManager>();

    }
    public virtual bool IsExecuteOK()
    {
        state = State.Prepare;
    }

    public virtual void Excute()
    {
        state = State.Prepare;
    }

    public virtual PlayerState Workflow()
    {
        PlayerStateMachine nextState = PlayerState;

        switch(state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                break;
            case State.Casting:
                break;
            case State.OnAction:
                break;
            case State.Finish:
                break;
            default;
                break;
        }
        return nextState;
    }

    enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }
}
