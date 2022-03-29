using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Slow : Buff
{
    public float slowPercent;
    EnemyMove enemyMove;
    float targetSpeedOrigin;


    public override void OnActive(Enemy target)
    {
        base.OnActive(enemy);
        enemyMove = target.GetComponent<EnemyMove>();
        targetSpeedOrigin = enemyMove.speed;
    }
    public override void OnDuration(Enemy target)
    {
        float tmpSpeed = targetSpeedOrigin * (1f - slowPercent / 100);
        if(enemyMove != null)
        if (enemyMove.speed != null)
            enemyMove.speed = tmpSpeed;

        return doBuff;
    }
    public override void OnDeactive(Enemy target)
    {
        base.OnDeactive(target);
    }

}
