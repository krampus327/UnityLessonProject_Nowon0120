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

    PlayerState playerState;
    
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
        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playerState = PlayerState.Jump;
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
                if (groundDetector.isGrounded)
                    playerState = PlayerState.Idle;
                break;
            default:
                break;
        }
    }
    enum PlayerState
    {
        Idle,
        Run,
        Jump,
    }
}
