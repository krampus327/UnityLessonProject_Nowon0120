using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    BoxCollider2D col;
    public float moveSpeed;
    public float jumpForce;
    Vector2 move; // direction vector (���� ����), ���⼭�� ũ�Ⱑ 1�� �Ѿ�� �����.

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
        get { return _direction; }
    }
    int directionInit = 1;

    // States    
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
    // Detectors
    PlayerGroundDetector groundDetector;

    // animation
    Animator animator;
    float animationTimeElapsed;
    float attackTime;
    float dashTime;
    float dashAttackTime;

    // kinematic
    public Vector2 knockBackForce;

    // casting
    public Vector2 attackCastingCenter;
    public Vector2 attackCastingSize;
    public Vector2 attackCastingDirection;
    public Vector2 attackTargetLayer;
    private void Awake()
    {
        player = GetComponent<Player>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        animator = GetComponentInChildren<Animator>();

        direction = directionInit;
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
    // Update is called once per frame
    void Update()
    {
        // Ű���� �¿� �Է� �޾Ƽ� ���ӿ�����Ʈ�� �¿�� �����̴� ���
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

            if (groundDetector.isGrounded &&
                jumpState == JumpState.Idle &&
                isAttacking == false)
            {
                if (Mathf.Abs(h) > 0.1f) // �����Է��� ������ 0���� ũ��
                {
                    if (playerState != PlayerState.Run) // �÷��̾���°� �޸������� ������
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
        }




        //rb.velocity = new Vector2(h * moveSpeed , 0); 
        // Rigidbody.velocity �� �������� �ֱ⸶�� ������ ��� 
        // ���������� ������ ����ų ���ɼ��� �����Ƿ� 
        // �ֱ��Լ������� velocity �� �ƴ϶� position �� �����ϴ� ������� �����δ�.
        // velocity �� ���� �����ϴ� ���� 
        // �����ϴ� ���� �� �� ��쿡 ���������� �ӵ��� �ٲ��� �Ҷ� 
        // �Ǵ� Ư�� ���ۿ��� �ٸ� �������� �Ѿ�� ���� �ӵ��� �缳�� �ؾ��Ҷ� ��������.

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ChangePlayerState(PlayerState.Jump);
        }

        if (playerState != PlayerState.Dash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangePlayerState(PlayerState.Dash);
        }
        Debug.Log($"{playerState}, {dashState}");
        // Attack actions
        if (isAttacking == false && Input.GetKeyDown(KeyCode.A))
        {
            PlayerState tmpStateToChange = PlayerState.Attack;
            if (dashState != DashState.Idle)
            {
                tmpStateToChange = PlayerState.DashAttack;
            }
            ChangePlayerState(tmpStateToChange);
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
    // �÷��̾��� ���°� �ٲ� 
    // �ʱ�ȭ �ؾ��ϴ� ��ҵ��� �ʱ�ȭ���ش�. 
    void ChangePlayerState(PlayerState stateToChange)
    {
        animationTimeElapsed = 0;
        switch (playerState)
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
                dashState = DashState.Idle;
                break;
            case PlayerState.DashAttack:
                dashAttackState = DashAttackState.Idle;
                break;
            default:
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
            case PlayerState.DashAttack:
                dashAttackState = DashAttackState.PrepareToDashAttack;
                break;
            default:
                break;
        }
        playerState = stateToChange;
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
                if (groundDetector.isGrounded == false)
                {
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (groundDetector.isGrounded)
                {
                    playerState = PlayerState.Idle;
                    jumpState = JumpState.Idle;
                    animator.Play("Idle");
                }
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
                    RaycastHit2D hit = Physics2D.BoxCast(rb.position + attackCastingCenter,
                        attackCastingSize, 0f, attackCastingDirection, attackTargetLayer);
                    if (hit.collider != null)
                    {
                        Enemy enemy = null;
                        if(hit.collider.TryGetComponent(out enemy))
                        {
                            enemy.hp = -=Attack
                        }
                    }
                    attackState = AttackState.Attacked;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            case AttackState.Attacked:
                playerState = PlayerState.Idle;
                attackState = AttackState.Idle;
                animationTimeElapsed = 0;
                animator.Play("Idle");
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
                    move.x = direction * moveSpeed * 1.5f;
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
                dashAttackState = DashAttackState.DashingAttacking;
                break;
            case DashAttackState.DashingAttacking:
                if (animationTimeElapsed < dashAttackTime * 1 / 4)
                {
                    move.x = direction * moveSpeed / 4;
                }
                else if (animationTimeElapsed < dashAttackTime * 3 / 4)
                {
                    move.x = direction * moveSpeed * 1.5f;
                }
                else
                {
                    dashAttackState = DashAttackState.DashAttacked;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            case DashAttackState.DashAttacked:
                if (animationTimeElapsed < dashAttackTime)
                {
                    move.x = direction * moveSpeed / 4;
                }
                else
                {
                    playerState = PlayerState.Idle;
                    dashAttackState = DashAttackState.Idle;
                    animationTimeElapsed = 0;
                    animator.Play("Idle");
                }
                animationTimeElapsed += Time.deltaTime;
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

    public void knockBack()
    {
        move = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(knockBackForce.x * (-direction), knockBackForce.y), ForceMode2D, Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Rigidbody2D.rigidbody = new Rigidbody2D();
        Gizmos.DrawWireCube(new Vector3(rigidbody.position.x + attackCastingCenter.x,
            rigidbody)
    }
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack,
        Dash,
        DashAttack,
    }
    public enum JumpState
    {
        Idle,
        PrepareToJump, // Jump �� �ʿ��� �Ķ���� ����, �ִϸ��̼� ��ȯ �� 
        Jumping, // Jump ���������� �����ϴ� �ܰ� 
        InFlight, // Jump ���������� ������ ���߿� ĳ���Ͱ� ���ִ� ����
    }
    public enum RunState
    {
        Idle,
        PrepareToRun,
        Running,
    }
    public enum AttackState
    {
        Idle,
        PrepareToAttack,
        Attacking,
        Attacked,
    }
    public enum DashState
    {
        Idle,
        PrepareToDash,
        Dashing,
        Dashed,
    }
    public enum DashAttackState
    {
        Idle,
        PrepareToDashAttack,
        DashingAttacking,
        DashAttacked,
    }
}