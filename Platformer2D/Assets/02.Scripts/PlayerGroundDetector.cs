using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D col;
    Vector2 center;
    public Vector2 size;
    //�ٸ� Ŭ���������� ������ �ؾ������� �ν�����â������ ����������
    [HideInInspector] public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        size.x = col.size.x / 2;
        size.y = 0.005f;
    }
    void Update()
    {
        center.x = rb.position.x + col.offset.x;
        center.y = rb.position.y + col.offset.y - col.size.y/2 - size.y;
        Physics2D.OverlapBox(center, size, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(center.x, center.y, 0), new Vector3(size.x, size.y, 0f));
    }
}
