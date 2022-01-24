using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // 카메라가 1등에게 따라붙게 하고싶다.
    //
    // 뭐가 필요할까?
    // 1. 카메라 자체의 Transform 컴포넌트
    // 2. 경주마들의 Transform 컴포넌트
    //
    // 쟤들로 뭘해야할까?
    // 1. 경주마들의 등수를 실시간으로 체크한다.
    // 2. 1등말의 위치를 가져온다.
    // 3. 카메라의 위치를 1등말의 위치에다가 특정거리만큼 떨어뜨린다.

    Transform tr;
    public List<Transform> list_Player;
    Transform leader;
    Vector3 origin;
    Vector3 dir;
    private void Awake()
    {

    }
    private void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        // 1등 말 체크하는 방법 : z축 값을 비교한다.
        // foreach 문이 돌때 그전요소의  Z축 값을 저장해두면 현재 foreach문 요소와 비교할수 있다.
        float prevZ = 0;
        leader = list_Player[0];
        foreach(Transform player in list_Player)
        {
            if(player.position.z > prevZ)
            {
                leader = player;
                prevZ = player.position.z;
            }
        }
        tr.position = leader.position;

    }
}
