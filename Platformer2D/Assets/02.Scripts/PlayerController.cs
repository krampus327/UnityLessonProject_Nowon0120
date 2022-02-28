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
    Vector2 move; // direction vector (���� ����), ���⼭�� ũ�Ⱑ 1�� �Ѿ�� �����

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
    public DashState dashState;
    public DashAttackState dashAttackState;
    public bool isAttacking
    {
        get
        {
            return (attackState != AttackState.Idle ||
                dashAttackState != DashAttackState.Idle);
        }
    }

    Animator animator;
    float animationTimeElapsed;
    float attackTime;
    float dashTime;
    float dashAttackTime;
    private void Awake()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        animator = GetComponentInChildren<Animator>();

        attackTime = GetAnimationTime("Attack");
        dashTime = GetAnimationTime("Dash");
        dashAttackTime = GetAnimationTime("DashAttack");
    }
    float GetAnimationTime(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
            {
                time = ac.animationClips[i].length; break;
            }
        }
        return time;
    }
    void Update()
    {
        //Ű���� �¿� �Է¹޾Ƽ� ���ӿ�����Ʈ�� �¿�� �����̴� ���
        float h = Input.GetAxis("Horizontal");
        if (IsChangeDirectionPossible())
        {
            if (h < 0)
                direction = -1;
            else if (h > 0)
                direction = 1;
        }

        if (IsHorizontalMovePossible())
        {
            move.x = h;
        }

        if (groundDetector.isGrounded &&
            jumpState == JumpState.Idle &&
            attackState == AttackState.Idle)
        {
            if (Mathf.Abs(h) > 0.1f) // �����Է��� ������ 0���� ũ��
            {
                if (playerState != PlayerState.Run) // �÷��̾ �޸������� ������
                {
                    playerState = PlayerState.Run; // �÷��̾���� �޸���� �ٲ�
                    runState = RunState.PrepareToRun; // �޸������ �޸��� �غ�� �ٲ�
                }
            }
            else // �����Է��� 0�̸�
            {
                h = 0;
                if (playerState != PlayerState.Idle) // �÷��̾���°� Idle�� �ƴϸ�
                {
                    playerState = PlayerState.Idle; // �÷��̾� ���¸� Idle��
                    animator.Play("Idle");
                }
            }
        }
        // rb.velocity = new Vector2(h * moveSpeed * Time.deltaTime, 0)
        //rigidbody.velocity �� �������� �ֱ⸶�� �����Ұ��
        //���������� ������ ����ų ���ɼ��� �����Ƿ�
        //�ֱ��Լ������� velocity�� �ƴ϶� position�� �����ϴ� ������� �����δ�
        //velocity�� ���� �����ϴ� ����
        //�����ϴ� ���� �� �� ��쿡 ���������� �ӵ��� �ٲ��� �Ҷ�
        //�Ǵ� Ư�����ۿ��� �ٸ� �������� �Ѿ�� ���� �ӵ��� �缳�� �ؾ��Ҷ� ��������

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            jumpState = JumpState.PrepareToJump;
            ChangePlayerState(PlayerState.Jump);
        }

        if (isAttacking == false && Input.GetKeyDown(KeyCode.A))
        {
            playerState = PlayerState.Attack;
            attackState = AttackState.PrepareToAttack;
        }

        if (playerState == PlayerState.Dash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerState = PlayerState.Dash;
            dashState = DashState.PrepareToDash;
        }
        //Attack actions
        if (isAttacking == false && Input.GetKeyDown(KeyCode.A))
        {
            playerState = PlayerState.Attack;
            attackState = AttackState.PrepareToAttack;

            if (dashState != DashState.Idle)
            {
                playerState = PlayerState.DashAttack;
                dashAttackState = DashAttackState.PrepareToDashAttack;
            }
        }

        if (isAttacking == false && playerState == PlayerState.Dash && Input.GetKeyDown(KeyCode.A))
        {
            playerState = PlayerState.DashAttack;
            dashAttackState = DashAttackState.PrepareToDashAttack;
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
    // �÷��̾��� ���°� �ٲ� �ʱ�ȭ �ؾ��ϴ� ��ҵ��� �ʱ�ȭ���ش�.
    void ChangePlayerState(PlayerState stateToChange)
    {
        animationTimeElapsed = 0;
        playerState = stateToChange;
        switch (PlayerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Jump:
                jumpState = JumpState.Idle;
                break;
            case PlayerState.Attack:
                attackState = AttackState.Idle;
                break;
            case PlayerState.Dash:
                dashAttackState = DashAttackState.Idle;
                break;
            case PlayerState.DashAttack:
                dashAttackState = DashAttackState.Idle;
                break;
        }
        switch (stateToChange)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Jump:
                jumpState = JumpState.PrepareToJump;
                break;
            case PlayerState.Attack:
                attackState = AttackState.PrepareToAttack;
                break;
            case PlayerState.Dash:
                dashState = DashState.PrepareToDash;
                break;
            case PlayerState.DashAttack;
                dashAttackState = DashAttackState.Idle;
                break;
        }
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
            case PlayerState.Dash:
                UpdateDashState();
                break;
            case PlayerState.DashAttack:
                UpdateDashAttackState();
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
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
                if (animationTimeElapsed > attackTime)
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
    void UpdateDashState()
    {
        switch (dashState)
        {
            case DashState.Idle:
                break;
            case DashState.PrepareToDash:
                animator.Play("Dash");
                dashState = DashState.Dashing;
                break;
            case DashState.Dashing:
                if (animationTimeElapsed < dashTime * 3 / 4)
                {
                    move.x = moveSpeed * 2f;
                }
                else
                {
                    dashState = DashState.Dashed;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            case DashState.Dashed:
                if (animationTimeElapsed > dashTime)
                {
                    playerState = PlayerState.Idle;
                    dashState = DashState.Idle;
                    animationTimeElapsed = 0;
                    animator.Play("Idle");
                }
                else
                {
                    move.x = direction * moveSpeed / 4f;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            default:
                break;
        }
    }
    void UpdateDashAttackState()
    {
        switch (dashAttackState)
        {
            case DashAttackState.Idle:
                break;
            case DashAttackState.PrepareToDashAttack:
                animator.Play("DashAttack");
                dashAttackState = DashAttackState.DashAttacking;
                break;
            case DashAttackState.DashAttacking:
                if (animationTimeElapsed < dashAttackTime
                break;
            case DashAttackState.DashAttacked:
                if(animationTimeElapsed > )
                break;
            default:
                break;
        }
    }
    private bool IsChangeDirectionPossible()
    {
        bool isOK = false;
        if (playerState == PlayerState.Idle ||
            playerState == PlayerState.Run ||
            playerState == PlayerState.Jump)
        {
            isOK = true;
        }
        return isOK;
    }

    private bool IsHorizontalMovePossible()
    {
        bool isOK = false;
        if (playerState == PlayerState.Idle ||
            playerState == PlayerState.Run)
        {
            isOK = true;
        }
        return isOK;
    }

    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack,
        Dash,
        DashAttack
    }

    public enum JumpState
    {
        Idle,
        PrepareToJump, // Jump�� �ʿ��� �Ķ���� ����, �ִϸ��̼� ��ȯ ��
        Jumping, // Jump���������� �����ϴ� �ܰ�
        InFlight, // Jump���������� ������ ���߿� ĳ���Ͱ� ���ִ� ����
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
    public enum DashState
    {
        Idle,
        PrepareToDash,
        Dashing,
        Dashed
    }
    public enum DashAttackState
    {
        Idle,
        PrepareToDashAttack,
        DashAttacking,
        DashAttacked

    }
}
