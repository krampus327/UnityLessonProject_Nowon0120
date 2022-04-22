using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AI ai;
    public bool aiJump;
    public float aiBehaviourTimeMax = 5f;
    public float aiBehaviourTimeMin = 1f;
    public float aiBehaviourTime;
    public float aiBehaviourTimer;

    public float moveSpeed = 2f;
    private Vector3 dir;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayAI();
    }

    public void PlayAI()
    {
        switch (ai)
        {
            case AI.DecideRandomBehaviour:
                ai = (AI)Random.Range(2, 4);
                aiBehaviourTimer = aiBehaviourTime = Random.Range(aiBehaviourTimeMin, aiBehaviourTimeMax);
                dir = Random.insideUnitSphere;
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                aiJump = Random.Range(0.0f, 1.0f) < 0.5f ? true : false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                break;
            case AI.Rest:
                if (aiBehaviourTimer < 0)
                    ai = AI.DecideRandomBehaviour;
                else
                    aiBehaviourTimer -= Time.deltaTime;
                break;
            case AI.Move:
                if (aiBehaviourTimer < 0)
                    ai = AI.DecideRandomBehaviour;
                else
                {
                    Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
                    Quaternion rotation = Quaternion.Look
                    rb.position += deltaMove
                    aiBehaviourTimer -= Time.deltaTime;
                }
                break;
            case AI.FollowTarget:
                break;
            case AI.AttackTarget:
                break;
            default:
                break;
        }
    }
    public enum AI
    {
        Idle,
        DecideRandomBehaviour,
        Rest,
        Move,
        FollowTarget,
        AttackTarget,
    }
}
