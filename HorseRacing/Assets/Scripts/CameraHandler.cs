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

    static public CameraHandler instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    Transform tr;
    Transform target;
    int targetIndex;
    public Vector3 offset;
    [SerializeField] private Transform platformCampoint;
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

    public void MoveToFlatform()
    {
        tr.position = platformCampoint.position;
        tr.rotation = platformCampoint.rotation;

    }
}
