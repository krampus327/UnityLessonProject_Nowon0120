using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public bool isGuided = false;
    public Transform targetGuide;
    public float speed;
    public Vector3 moveVec;
    Transform tr;
    private void Awake()
    {
        tr = transform;
    }
    private void FixedUpdate()
    {
        if (isGuided)
        {
            tr.LookAt(targetGuide);
            moveVec = (targetGuide.position - tr.position).normalized * speed;
        }
        tr.Translate(moveVec);
    }
    public void SetMoveVector(Vector3 cos)
    {
        moveVec
    }
}