using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Idle : PlayerStateMachine
{
    public override PlayerState UpdateState()
    {
        PlayerState nextPlayerState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animator.Play("Idle");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                break;
            case State.Finish:
                break;
            default:
                break;
        }
        return nextPlayerState;
    }
}
