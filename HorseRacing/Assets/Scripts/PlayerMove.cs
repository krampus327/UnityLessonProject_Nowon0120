using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform tr;
    public float distance;
    public Vector3 dir;
    public float minSpeed;
    public float maxSpeed;
    public bool doMove;

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        RacingPlay.instance.Register(this);
        // instance의 초기화하는 Awake 함수에서, instasnce에 접근하는 구문은 Start함수에서 일반적으로 수행한다.
    }



    // Update is called once per frame
    void Update()
    {
        if(doMove)
            Move();
    }
    private void Move()
    {
        float moveSpeed = Random.Range(minSpeed, maxSpeed);
        Vector3 moveVec = dir * moveSpeed * Time.deltaTime;
        tr.Translate(moveVec);
        distance += moveVec.magnitude;
    }
}
