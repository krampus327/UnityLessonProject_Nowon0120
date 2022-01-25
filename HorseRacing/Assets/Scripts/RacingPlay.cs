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
    private int grade;

    public void Register(PlayerMove playerMove)
    {
        list_PlayerMove.Add(playerMove);
        Debug.Log($"{playerMove.gameObject.name} (��)�� ��� �Ϸ� �Ǿ����ϴ� ���� �� ��ϼ� : {list_PlayerMove.Count}");
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
}
