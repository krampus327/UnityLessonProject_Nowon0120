using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    CapsuleCollider2D col;
    PlayerGroundDetector groundDetector;
    public float moveSpeed;
    public float jumpForce;
    Vector2 move; // direction vector (방향 벡터), 여기서는 크기가 1이 넘어가도 사용함

    int _direction;
    int direction
    {
        set
        {
            _direction = value;
            if (_direction < 0)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            else if (_direction > 0)
                transform.eulerAngles = Vector3.zero;
        }
        get
        {
            return _direction;
        }
    }

    public PlayerState playerState;
    public JumpState jumpState;
    public RunState runState;
    public AttackState attackState;

    Animator animator;
    float animationTimeElapsed;
    float attackTime;
    private void Awake()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        animator = GetComponentInChildren<Animator>();

        attackTime = GetAnimationTime("Attack");
    }
    float GetAnimationTime(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if(ac.animationClips[i].name == name)
            {
                time = ac.animationClips[i].length; break;
            }
        }
        return time;
    }
    void Update()
    {
        //키보드 좌우 입력받아서 게임오브젝트를 좌우로 움직이는 기능
        float h = Input.GetAxis("Horizontal");
        if (h < 0)
            direction = -1;
        else if (h > 0)
            direction = 1;

        move.x = h;

        if (groundDetector.isGrounded &&
            jumpState == JumpState.Idle &&
            attackState == AttackState.Idle)
        {
            if(Mathf.Abs(h) > 0.1f) // 수평입력의 절댓값이 0보다 크면
            {
                if (playerState != PlayerState.Run) // 플레이어가 달리고있지 않으면
                {
                    playerState = PlayerState.Run; // 플레이어상태 달리기로 바꿈
                    runState = RunState.PrepareToRun; // 달리기상태 달리기 준비로 바꿈
                }
            }
            else // 수평입력이 0이면
            {
                h = 0;
                if(playerState != PlayerState.Idle) // 플레이어상태가 Idle이 아니면
                {
                    playerState = PlayerState.Idle; // 플레이어 상태를 Idle로
                    animator.Play("Idle");
                }   
            }
        }
        // rb.velocity = new Vector2(h * moveSpeed * Time.deltaTime, 0)
        //rigidbody.velocity 를 물리연산 주기마다 실행할경우
        //비정상적인 동작을 일으킬 가능성이 있으므로
        //주기함수에서는 velocity가 아니라 position을 변경하는 방식으로 움직인다
        //velocity를 직접 수정하는 경우는
        //점프하는 순간 등 의 경우에 순간적으로 속도가 바뀌어야 할때
        //또는 특정동작에서 다른 동작으로 넘어가는 순간 속도를 재설정 해야할때 직접수정

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerState = PlayerState.Jump;
            jumpState = JumpState.PrepareToJump;
        }

        if (playerState != PlayerState.Attack&& Input.GetKeyDown(KeyCode.A))
        {
            playerState = PlayerState.Attack;
            attackState = AttackState.PrepareToAttack;
        }
        UpdatePlayerState();
    }

    private void FixedUpdate()
    {
        FixedUpdateMovement();
    }
    void FixedUpdateMovement()
    {
        Vector2 velocity = new Vector2(move.x * moveSpeed, move.y);
        rb.position += velocity * Time.fixedDeltaTime;
    }
    void UpdatePlayerState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                UpdateRunState();
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            case PlayerState.Attack:
                UpdateAttackState();
                break;
            default:
                break;
        }
    }
    void UpdateRunState()
    {
        switch (runState)
        {
            case RunState.PrepareToRun:
                animator.Play("Run");
                runState = RunState.Running;
                break;
            case RunState.Running:
                break;
        }
    }
    void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                animator.Play("Jump");
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
                jumpState = JumpState.Jumping;
                break;
            case JumpState.Jumping:
                jumpState = JumpState.InFlight;
                break;
            case JumpState.InFlight:
                if (groundDetector.isGrounded &&
                    animationTimeElapsed > 0.1f)
                {
                    playerState = PlayerState.Idle;
                    jumpState = JumpState.Idle;
                    animationTimeElapsed = 0;
                    animator.Play("Idle");
                }
                animationTimeElapsed += Time.deltaTime;
                break;
        }
    }

    void UpdateAttackState()
    {
        switch (attackState)
        {
            case AttackState.PrepareToAttack:
                animator.Play("Attack");
                attackState = AttackState.Attacking;
                break;
            case AttackState.Attacking:
                if(animationTimeElapsed > attackTime)
                {
                    attackState = AttackState.Attacked;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            case AttackState.Attacked:
                playerState = PlayerState.Idle;
                attackState = AttackState.Idle;
                animator.Play("Idle");
                animationTimeElapsed = 0;
                break;
        }
    }

    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack
    }

    public enum JumpState
    {
        Idle,
        PrepareToJump, // Jump에 필요한 파라미터 세팅, 애니메이션 전환 등
        Jumping, // Jump물리연산을 시작하는 단계
        InFlight, // Jump물리연산이 끝나고 공중에 캐릭터가 떠있는 상태
    }
    public enum RunState
    {
        Idle,
        PrepareToRun,
        Running
    }
    public enum AttackState
    {
        Idle,
        PrepareToAttack,
        Attacking,
        Attacked
    }
}
