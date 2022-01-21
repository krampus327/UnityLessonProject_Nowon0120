using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    public List<GameObject> list_Player = new List<GameObject> (); // 플레이어를 담을 리스트
    public float minSpeed = 0; // 랜덤속도 생성 최소값
    public float maxSpeed = 10; // 랜덤속도 생성 최대값
    public Transform goalLine;
    public Transform platform1Grade;
    public Transform platform2Grade;
    public Transform platform3Grade;

    List<GameObject> list_Finished = new List<GameObject> (); // 끝난 플레이어를 저장할 리스트
    bool isRacingFinished = false; // 경주 끝났는지 체크할 변수
    private void Update()
    {
        // 경주가 안끝났으면 ~
        if (isRacingFinished == false) 
        {
            GameObject tmpFinishedPlayer = null;
            // 플레이어 리스트 반복
            foreach (GameObject player in list_Player)
            {
                float moveSpeed = Random.Range(minSpeed, maxSpeed); // 랜덤 속도 생성
                Transform tmpTr = player.GetComponent<Transform>(); // 현재 플레이어으 Transform 컴포넌트 가져옴
                float moveDistance = moveSpeed * Time.deltaTime; // 속도에 델타타임 곱해서 거리계산 + 프레임수 차이에 의한 이동거리차이 없앰
                Vector3 moveVector = Vector3.forward * moveDistance; // forward 단위벡터에 이동할 거리 곱해서 이동할 벡터 구함
                tmpTr.Translate(moveVector); // 이동할 벡터만큼 이동하라는 명령

                // 결승점 넘었으면 ~ 
                if(tmpTr.position.z > goalLine.position.z)
                {
                    list_Finished.Add(player);
                    tmpFinishedPlayer = player;
                }
            }
            list_Player.Remove(tmpFinishedPlayer);
            // 플레이어 5명이 모두 들어왔으면 ~ 
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
