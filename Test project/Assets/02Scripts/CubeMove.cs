using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    Transform tr;
    // public ���� �����ڸ� ����ϸ� inspector â�� �����̵ȴ�.
    // ���࿡ �ٸ� Ŭ�����κ����� ������ �����ϸ鼭 inspectorâ�� �����Ű�� ������ [SerializeField] �Ӽ��� ����Ѵ�.
    [SerializeField] private float speed = 1; // inspector â�� ���� �ʱ�ȭ ������ �켱������.
    [SerializeField] private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // transform �� �����ؼ� ��ǥ�� ���� �����͸� ������ѵ� ������ ���� transform �m������ tr�� �����ؼ�
        // Transform component �� ������ �Ŀ� ����ϴ� ����
        // ĳ�� �޸� ���� ���� (ĳ�� : �ӽ÷� ������ �����ϵ��� ������ �޸�)
        // transform �� ����ϸ� �� ��������� ȣ���� �� ���� gameOfject �� �����ؼ� getComponent �� Transform ������ ������
        // ������ Transform ������� tr ���ٰ� �ѹ� �־���� ����ϸ�
        // tr�� ����Ҷ����� ó���� �־���� Transform component ���� �ٷ� �����ϱ� ������
        // ���ÿ� ���־��� ���� ���ӿ�����Ʈ���� Transform ������Ʈ�� ����ϸ� �׶��� �����ս����� ���̰� ����

        tr = gameObject.GetComponent<Transform>(); // ���ӿ�����Ʈ ������ Transform ������Ʈ�� �����´�
        tr = this.gameObject.GetComponent<Transform>(); // �� Ŭ������ �����ϴ� ���ӿ�����Ʈ���Լ� transform ������Ʈ�� �����´�
        //tr = this.gameObject.transform; // ���ӿ�����Ʈ�� ������� transform�� �����Ѵ�. (������� transform�� ���� ����ִ��� ����������
        //tr = gameObject.transform;
    }

    // Update is called once per frame
    // Upsdate �� �� �����Ӹ��� ȣ��Ǵ� �Լ�.
    void Update()
    {
        // 1�����Ӵ� z �� 1 ����
        // ���࿡ ��ǻ�� ����� �޶� �ϳ��� 60FPS, �ٸ� �ϳ��� 30FPS
        // -> 1�ʿ� �ϳ��� 60��ŭ �����ϰ�, �ٸ� �ϳ��� 30��ŭ �����ϰԵ�
        //tr.position += new Vector3(0, 0, 1);
        // Time.deltaTime ���� �����Ӱ� ���� ������ ���� �ɸ��ð�
        // �� Time.deltaTime �� �����ָ� ��⼺�ɿ� ������� �ʴ� ���� ��ȭ���� ���� �� �ִ�
        //tr.position += new Vector3(0, 0, speed) * Time.deltaTime;

        // Physics ���� �����͸� ó���Ҷ� ���� �����
        //tr.position += new Vector3(0, 0, 1) * Time.fixedDeltaTime;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h =" + h);
        Debug.Log($"v = {v}");
        // Z exis forward, back
        // X exis left, right
        // Y axis up, down

        //�Ʒ�ó�� ���� ���ÿ� ���������� ���������� ���⺤���� ũ�Ⱑ 1�� �Ѿ�� ���⿡���� �ӵ��� �������� �ʴ�.
        //Vector3 movePos = new Vector3(h, 0, v) * speed * Time.deltaTime;
        //tr.Translate(movePos);

        // Vector ����� ũ�⸦ ��� ������ ����
        // Ư�� Vector ũ�Ⱑ 1��  ���͸� �������� (Unit Vector)
        // �����̰� ���� ���⿡ ���� �������� * �ӵ� �� ��ü�� ������
        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 moveVec = dir * speed * Time.deltaTime;
        //
        //tr.Translate(moveVec);

        //tr.Translate(moveVec, Space.Self); // local ��ǥ�� ���� �̵�
        tr.Translate(moveVec, Space.World); // World ��ǥ�� ���� �̵�

        // Rotation
        // ==========================================================================
        //tr.Rotate(new Vector3(0f, Mathf.Deg2Rad * 30f, 0f)); // Y������ 30 radian ��ŭ ȸ���϶� Degree 0~360 ���� ��Ÿ���� ���� RAdian 0

        float r = Input.GetAxis("Mouse X");
        Vector3 rotateVec =  Vector3.up * rotateSpeed * r * Time.deltaTime;
        tr.Rotate(rotateVec);
    }
    // FicedUpdate�� ���� �����Ӹ��� ȣ��Ǵ� �Լ�
    private void FixedUpdate()
    {
        
    }
}
