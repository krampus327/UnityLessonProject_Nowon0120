using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    int _direction;
    int direction
    {
        set
        {
            _direction = value;
            if (_direction < 0)
                transform.eulerAngles = Vector3.zero;
            else if (_direction > 0)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        get { return _direction; }
    }
    int directionInit = -1;

    //states
    public EnemyState enemyState;
    public HurtState hurtState;
    public DieState dieState;

    // animation
    Animator animator;
    float animationTimeElapsed;
    float hurtTime;
    float dieTime;

    // movement
    Rigidbody2D rb;
    CapsuleCollider2D col;
    Vector2 move;

    // AI
    public AIState aiState;
    private float aiStateTime;
    private float aiStateTimeElapsed;
    public bool autoFollow;
    public float autoFollowRangeRadius;
    public LayerMask playerLayer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        hurtTime = GetAnimationTime("Hurt");
        dieTime = GetAnimationTime("Die");
    }
    float GetAnimationTime(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
            {
                time = ac.animationClips[i].length; break;
            }
        }
        return time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // todo -> UpdataAI();
        if (move.x < 0) direction = -1;
        else if (move.x > 0) direction = 1;

        if (IsHorizontalMovePossible())
        {
            if (Mathf.Abs(move.x) > 0.1f)
                ChangeEnemyState(EnemyState.Move);
            else
                ChangeEnemyState(EnemyState.Idle);
        }
        UpdateEnemyState();
    }
    private void UpdateAI()
    {
        if (autoFollow)
        {
            Collider2D targetCol = Physics2D.OverlapCircle(rb.position, autoFollowRangeRadius, playerLayer);
            if (targetCol != null)
                aiState = AIState.FollowTarget;
        }
        else
        {
            if (enemyState == EnemyState.Hurt)
                aiState = AIState.FollowTarget;
        }
        switch (aiState)
        {
            case AIState.DecideRandomBehavior:
                aiStateTime = Random.Range(1f, 5f);
                aiStateTimeElapsed = 0f;
                aiState = (AIState)Random.Range(1, 4);
                break;
            case AIState.TakeARest:
                if(aiStateTimeElapsed > aiStateTime)
                    aiState = AIState.DecideRandomBehavior;
                aiStateTimeElapsed += Time.deltaTime;
                break;
            case AIState.MoveRight:
                if (aiStateTimeElapsed > aiStateTime)
                    aiState = AIState.DecideRandomBehavior;
                else
                    move.x = 1;
                aiStateTimeElapsed += Time.deltaTime;
                break;
            case AIState.MoveLeft:
                if (aiStateTimeElapsed > aiStateTime)
                    aiState = AIState.DecideRandomBehavior;
                else
                    move.x = -1;
                aiStateTimeElapsed += Time.deltaTime;
                break;
            case AIState.FollowTarget:
                Collider2D targetCol = Physics2D.OverlapCircle(rb.position, autoFollowRangeRadius);
                if(targetCol == null)
                    aiState = AIState.DecideRandomBehavior;
                else
                {
                    Transform targetTransform = targetCol.transform;
                    if (targetTransform.position.x > rb.position.x + col.size.x)
                        move.x = 1;
                    else if (targetTransform.position.x > rb.position.x - col.size.x)
                        move.x = -1;
                }
                break;
        }
    }

    private void ChangeEnemyState(EnemyState stateToChange)
    {
        if (enemyState == stateToChange) return;
        enemyState = stateToChange;
        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.Play("Idle");
                break;
            case EnemyState.Move:
                animator.Play("Move");
                break;
            case EnemyState.Hurt:
                animator.Play("Hurt");
                break;
            case EnemyState.Die:
                animator.Play("Die");
                break;
            default:
                break;
        }
    }
    private void UpdateEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Hurt:
                UpdateHurtState();
                break;
            case EnemyState.Die:
                UpdateDieState();
                break;
            case EnemyState.Move:
                break;
        }
    }
    private void UpdateHurtState()
    {
        switch (hurtState)
        {
            case HurtState.Idle:
                break;
            case HurtState.PrepareToHurt:
                hurtState = HurtState.Hurting;
                break;
            case HurtState.Hurting:
                if(animationTimeElapsed > hurtTime)
                {
                    hurtState = HurtState.Hurted;
                }
                break;
            case HurtState.Hurted:
                hurtState = HurtState.Idle;
                break;
            default:
                break;
        }
    }
    private void UpdateDieState()
    {
        switch (dieState)
        {
            case DieState.Idle:
                break;
            case DieState.PrepareToDie:
                dieState = DieState.Dying;
                break;
            case DieState.Dying:
                if(animationTimeElapsed > dieTime)
                {
                    dieState = DieState.Dead;
                }
                break;
            case DieState.Dead:
                dieState = DieState.Idle;
                break;
        }

    }
    private void FixedUpdate()
    {
        FixedUpdateMovement();
    }
    void FixedUpdateMovement()
    {
        Vector2 velocity = new Vector2(move.x * moveSpeed, move.y);
        rb.position += velocity * Time.fixedDeltaTime;
    }
    private bool IsHorizontalMovePossible()
    {
        bool isOK = false;
        if (enemyState == EnemyState.Idle || enemyState == EnemyState.Move)
            isOK = true;
        return isOK;
    }
    public enum EnemyState
    {
        Idle,
        Move,
        Hurt,
        Die
    }
    public enum MoveState
    {

    }
    public enum HurtState
    {
        Idle,
        PrepareToHurt,
        Hurting,
        Hurted
    }
    public enum DieState
    {
        Idle,
        PrepareToDie,
        Dying,
        Dead
    }
    public enum AIState
    {
        DecideRandomBehavior,
        TakeARest,
        MoveLeft,
        MoveRight,
        FollowTarget
    }
}
