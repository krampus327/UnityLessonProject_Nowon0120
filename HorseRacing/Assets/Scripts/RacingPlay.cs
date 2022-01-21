using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    public List<GameObject> list_Player = new List<GameObject> (); // �÷��̾ ���� ����Ʈ
    public float minSpeed = 0; // �����ӵ� ���� �ּҰ�
    public float maxSpeed = 10; // �����ӵ� ���� �ִ밪
    public Transform goalLine;
    public Transform platform1Grade;
    public Transform platform2Grade;
    public Transform platform3Grade;

    List<GameObject> list_Finished = new List<GameObject> (); // ���� �÷��̾ ������ ����Ʈ
    bool isRacingFinished = false; // ���� �������� üũ�� ����
    private void Update()
    {
        // ���ְ� �ȳ������� ~
        if (isRacingFinished == false) 
        {
            GameObject tmpFinishedPlayer = null;
            // �÷��̾� ����Ʈ �ݺ�
            foreach (GameObject player in list_Player)
            {
                float moveSpeed = Random.Range(minSpeed, maxSpeed); // ���� �ӵ� ����
                Transform tmpTr = player.GetComponent<Transform>(); // ���� �÷��̾��� Transform ������Ʈ ������
                float moveDistance = moveSpeed * Time.deltaTime; // �ӵ��� ��ŸŸ�� ���ؼ� �Ÿ���� + �����Ӽ� ���̿� ���� �̵��Ÿ����� ����
                Vector3 moveVector = Vector3.forward * moveDistance; // forward �������Ϳ� �̵��� �Ÿ� ���ؼ� �̵��� ���� ����
                tmpTr.Translate(moveVector); // �̵��� ���͸�ŭ �̵��϶�� ���

                // ����� �Ѿ����� ~ 
                if(tmpTr.position.z > goalLine.position.z)
                {
                    list_Finished.Add(player);
                    tmpFinishedPlayer = player;
                }
            }
            list_Player.Remove(tmpFinishedPlayer);
            // �÷��̾� 5���� ��� �������� ~ 
            if(list_Finished.Count >= 5)
            {
                isRacingFinished = true;
                list_Finished[0].transform.GetComponent<Transform>().position = platform1Grade.position;
                list_Finished[1].transform.GetComponent<Transform>().position = platform2Grade.position;
                list_Finished[2].transform.GetComponent<Transform>().position = platform3Grade.position;
            }
        }
    }
}
