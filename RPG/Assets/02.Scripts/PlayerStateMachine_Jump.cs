using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Jump : MonoBehaviour
{
    public float deltaMove = 0.01f;

    private CharacterController controller;
    private PlayerMove playerMove;
    private float detectGroundTimeLimit = 1f;
    private float detectGroundTimer;
    private float jumpUpTime;
    private float jumpUpTimer;
    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        playerMove = GetComponent<PlayerMove>();
    }

    public override bool IsExecuteOK()
    {
        if((manager.controller.playerState == PlayerState.Idle ||
            manager.controller.playerState == PlayerState.Move) &&
            manager.controller.isGrounded)
            return true;
        return false;
    }
    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                PlayerAnimator.SetTrigger("doJump");
                jumpUpTimer = jumpUpTime;
                detectGroundTimer = detectGroundTimeLimit;
                state++;
                break;
            case State.Casting:
                if (controller.isGrounded == false)
                    state++;
                else if (detectGroundTimer < 0)
                    state = State.Finish;
                else
                    detectGroundTimer -= Time.deltaTime;
                playerMove.SetMove(deltaMove);
                break;
            case State.OnAction:
                if (controller.velocity.y < 0)
                {
                    playerAnimator.SetTrigger("doFall");
                }
                if (controller.isGrounded)
                    state++;

                if(jumpUpTimer > 0)
                {
                    playerMove.SetMove(deltaMove);
                    jumpUpTimer -= Time.deltaTime;
                }
                break;
            case State.Finish:
                nextState = PlayerState.Idle;
                state = State.Idle;
                break;
            default;
                break;
        }
    }
}
