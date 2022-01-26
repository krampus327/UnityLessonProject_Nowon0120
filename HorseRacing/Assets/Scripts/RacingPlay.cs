using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    #region singleton (�̱���) ����
    static public RacingPlay instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    #endregion

    private List<PlayerMove> list_PlayerMove = new List<PlayerMove> ();
    private List<Transform> list_FinishedPlayer = new List<Transform>();
    [SerializeField] private List<Transform> list_WinPlatform = new List<Transform>();
    private int totalPlayerNum;
    private int grade;
    [SerializeField] Transform goal;

    private void Update()
    {
        CheckPlayerReachedToGoalAndStopMove();
    }

    public void Register(PlayerMove playerMove)
    {
        list_PlayerMove.Add(playerMove);
        totalPlayerNum++;
        Debug.Log($"{playerMove.gameObject.name} (��)�� ��� �Ϸ� �Ǿ����ϴ� ���� �� ��ϼ� : {list_PlayerMove.Count}");
    }
    public void StartRacing()
    {
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            playerMove.doMove = true;
        }
    }
    private void CheckPlayerReachedToGoalAndStopMove()
    {
        PlayerMove tmpFinishedPlayerMove = null;
        foreach(PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.transform.position.z > goal.position.z)
            {
                tmpFinishedPlayerMove = playerMove;
                break;
            }
        }
        //�÷��̾ ��ǥ������ ����������
        if(tmpFinishedPlayerMove != null)
        {
            tmpFinishedPlayerMove.doMove = false;
            list_FinishedPlayer.Add(tmpFinishedPlayerMove.transform);
            list_PlayerMove.Remove(tmpFinishedPlayerMove);
        }
        //���ְ� �����ٸ� (��� �÷��̾ ��ǥ�� ������� ��)
        if(list_FinishedPlayer.Count == totalPlayerNum)
        {
            //1,2,3���� �ܻ� ��ġ��Ű�� ī�޶�� �ܻ��� �ﵵ�� �Ѵ�.
            for (int i = 0; i < list_WinPlatform.Count; i++)
            {
                list_FinishedPlayer[i].position = list_WinPlatform[i].position;
            }
        }
    }
    public Transform GetPlayer(int index)
    {
        // �Լ��� ��ȯ�� ���������� ����� �ʱ�ȭ�� �Լ��� ���� ��ܿ� �Ѵ�.
        Transform tmpPlayerTransform = null;
        // �Լ�����: ���꿡 ���� ��ȯ�� ���������� ���� �����Ѵ�.
        if (index < list_PlayerMove.Count)
        {
            tmpPlayerTransform = list_PlayerMove[index].transform;
            // �Լ��� ���� �ϴܿ��� ���������� ��ȯ�Ѵ�.
            return tmpPlayerTransform;
        }
        else
        {
            return null;
        }
    }
    public Transform Get1GradePlayer()
    {
        Transform leader = list_PlayerMove[0].gameObject.GetComponent<Transform>();
        float prevDistance = list_PlayerMove[0].distance;
        foreach(PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.distance > prevDistance)
            {
                prevDistance = playerMove.distance;
                leader = playerMove.gameObject.GetComponent<Transform>();
            }
        }
        return leader;
    }
    public int GetTotalPlayerNumber()
    {
        return list_PlayerMove.Count;
    }
}
