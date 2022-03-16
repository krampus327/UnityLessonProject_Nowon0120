using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform tr;

    public int wayPointIndex;
    public float speed = 1f;
    public Transform nextWayPoint;
    public float originPosY;
    private void Awake()
    {
        tr = transform;
        originPosY = tr.position.y;
    }
    private void Start()
    {
        nextWayPoint = WayPoints.points[0];
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(nextWayPoint.position.x, originPosY, nextWayPoint.position.z);
        Vector3 dir = (nextWayPoint.position - tr.position).normalized;
        
        if(Vector3.Distance(tr.position, targetPos) < 0.1f)
        {
            if (WayPoints.TryGetNextWayPoint(wayPointIndex, out nextWayPoint))
            {
                wayPointIndex++;
            }
            else
            {
                // hurt player & destroy this
            }
        }

        tr.Translate(dir * speed * Time.fixedDeltaTime, Space.World);
    }


}
