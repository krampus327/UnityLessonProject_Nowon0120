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

    float animationTimeElapsed;
    private void Awake()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
    }
    void Update()
    {
        //Ű���� �¿� �Է¹޾Ƽ� ���ӿ�����Ʈ�� �¿�� �����̴� ���
        float h = Input.GetAxis("Horizontal");
        if (h < 0)
            direction = -1;
        else if (h > 0)
            direction = 1;
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
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            default:
                break;
        }
    }
    void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                //todo -> changeAnimation
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
}
