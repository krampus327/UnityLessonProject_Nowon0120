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
    Transform target;
    int targetIndex;
    public Vector3 offset;
    private void Awake()
    {

    }
    private void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        if(Input.GetKeyDown("tab"))
            SwitchNextTarget();
        if (target == null)
            SwitchNextTarget();
        else
            tr.position = target.position + offset;
    }
    public void SwitchNextTarget()
    {
        targetIndex++;
        if (targetIndex > RacingPlay.instance.GetTotalPlayerNumber() - 1)
            targetIndex = 0;
        target = RacingPlay.instance.GetPlayer(targetIndex);
    }
    public void SwitchTargetTo1Grade()
    {
        target = RacingPlay.instance.Get1GradePlayer();
    }
}
