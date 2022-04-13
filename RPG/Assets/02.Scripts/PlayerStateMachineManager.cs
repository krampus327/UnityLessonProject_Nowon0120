using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineManager : MonoBehaviour
{
    public float turnSpeed = 1f;

    Vector3 direction;
    Vector3 move;
    Coroutine turnCoroutine = null;
    Animator animator;
    Rigidbody rb;
    Transform tr;

    Vector3 targetAngle;
    bool isMove;

    Vector3 angle;
    Vector3 oldPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 tmpDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // 1.���࿡ �÷��̾� Y�� ���� 0 �ƴϸ� 0�ɶ����� ȸ��
            // 2.������ ����
            tmpDir = tr.forward;
            targetAngle = new Vector3(0,0,0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // 1.���࿡ �÷��̾� Y�� ���� 180 �ƴϸ� 180�ɶ����� ȸ��
            // 2.�ڷ� ����
            tmpDir = -tr.forward;
            targetAngle = new Vector3(0,180,0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 1.���࿡ �÷��̾� Y�� ���� 270 �ƴϸ� 270�ɶ����� ȸ��
            // 2.�������� ����
            tmpDir = (tmpDir + -tr.right).normalized;
            targetAngle = new Vector3(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // 1.���࿡ �÷��̾� Y�� ���� 90 �ƴϸ� 90�ɶ����� ȸ��
            // 2.���������� ����
            tmpDir = (tmpDir + tr.right).normalized;
            targetAngle = new Vector3(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // �����ϱ�
        }

        targetAngle /= 90;
        targetAngle += tr.eulerAngles;

    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            targetAngle *= turnSpeed * Time.fixedDeltaTime;
            targetAngle *= tr.eulerAngles;
            tr.eulerAngles = new Vector3(tr.eulerAngles.x, Mathf.Lerp(tr.eulerAngles.x, targetAngle.y, 0.5f), tr.eulerAngles.z);
        }

    }
}

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Fall,
}
