using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int _hp;
    public int hp
    {
        set
        {
            int tmpValue = value;
            if (tmpValue <= 0)
                tmpValue = 0;
            _hp = tmpValue;
        }
        get { return _hp; }
    }
    public int hpMax;

    public bool invincible = false;
    public float invincibleTime = 0.5f;

    Coroutine coroutine;
    private void Awake()
    {
        hp = hpMax;
    }
    public void Hurt(int damage)
    {
        if (invincible) return;

        if(coroutine != null)
            StopCoroutine(coroutine);
        StartCoroutine(E_SetInvincible());

        hp -= damage;
    }
    IEnumerator E_SetInvincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        coroutine = null;
    }
}
