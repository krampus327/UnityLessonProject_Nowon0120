using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rockat : MonoBehaviour
{
    public bool isGuided = false;
    public Transform targetGuide;
    public float speed;
    private int damage;
    public LayerMask touchLayer;
    public LayerMask targetLayer;
    public float explosionRange;

    public Vector3 moveVec;
    Transform tr;
    private void Awake()
    {
        tr = transform;
    }
    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(tr.position, 1f, touchLayer);
        if (cols.Length > 0)
            Explode();
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
    public void SetMoveVector(Vector3 dir, Transform target)
    {
        moveVec = dir * speed;
        targetGuide = target;
        _damage = damage;
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
    private void Explode()
    {
        Collider[] enemiesCol = Physics.OverlapSphere(tr.position, explosionRange, targetLayer);
        foreach(var enemyCol in enemiesCol)
        {
            enemyCol.GetComponent<Enemy>().hp -= damage;
        }
        Destroy(gameObject);
    }
    private void Attack()
    {
        foreach (var firePoint in firePoints)
        {
            GameObject Rocket = Instantiate(RocketPrefab, firePoint.position, Quaternion.identity);
            Vector3 dir = (Target.transform.position - Rocket.transform.position).normalized;
            Rocket.GetComponent<Rocket>().SetMoveVector(dir, Target, damage);
        }
    }
}