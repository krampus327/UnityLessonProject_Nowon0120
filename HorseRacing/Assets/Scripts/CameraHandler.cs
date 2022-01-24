using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // ī�޶� 1��� ����ٰ� �ϰ�ʹ�.
    //
    // ���� �ʿ��ұ�?
    // 1. ī�޶� ��ü�� Transform ������Ʈ
    // 2. ���ָ����� Transform ������Ʈ
    //
    // ����� ���ؾ��ұ�?
    // 1. ���ָ����� ����� �ǽð����� üũ�Ѵ�.
    // 2. 1��� ��ġ�� �����´�.
    // 3. ī�޶��� ��ġ�� 1��� ��ġ���ٰ� Ư���Ÿ���ŭ ����߸���.

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
        // 1�� �� üũ�ϴ� ��� : z�� ���� ���Ѵ�.
        // foreach ���� ���� ���������  Z�� ���� �����صθ� ���� foreach�� ��ҿ� ���Ҽ� �ִ�.
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
