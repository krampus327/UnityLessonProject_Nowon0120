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

    Animator animator;
    float animationTimeElapsed;
    private void Awake()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //Ű���� �¿� �Է¹޾Ƽ� ���ӿ�����Ʈ�� �¿�� �����̴� ���
        float h = Input.GetAxis("Horizontal");
        if (h < 0)
            direction = -1;
        else if (h > 0)
            direction = 1;

        if(groundDetector.isGrounded &&
            jumpState == JumpState.Idle)
        {
            if(Mathf.Abs(h) > 0.1f) // �����Է��� ������ 0���� ũ��
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
                if(playerState != PlayerState.Idle) // �÷��̾���°� Idle�� �ƴϸ�
                {
                    playerState = PlayerState.Idle; // �÷��̾� ���¸� Idle��
                    animator.Play("Idle");
                }   
            }
        }
        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);
        // rb.velocity = new Vector2(h * moveSpeed * Time.deltaTime, 0)
        //rigidbody.velocity �� �������� �ֱ⸶�� �����Ұ��
        //���������� ������ ����ų ���ɼ��� �����Ƿ�
        //�ֱ��Լ������� velocity�� �ƴ϶� position�� �����ϴ� ������� �����δ�
        //velocity�� ���� �����ϴ� ����
        //�����ϴ� ���� �� �� ��쿡 ���������� �ӵ��� �ٲ��� �Ҷ�
        //�Ǵ� Ư�����ۿ��� �ٸ� �������� �Ѿ�� ���� �ӵ��� �缳�� �ؾ��Ҷ� ��������

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerState = PlayerState.Jump;
            jumpState = JumpState.PrepareToJump;
        }
        UpdatePlayerState();
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

    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
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
}
