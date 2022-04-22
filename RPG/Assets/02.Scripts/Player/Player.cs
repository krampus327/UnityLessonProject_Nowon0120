using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hpMax;
    private float _hp;
    private float hp
    {
        get
        {
            if(value < 0)
            {
                value = 0;
                // do die
            }
            _hp = value;
            
            if(PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_hp / hpMax);
            }
        }
        get
        {
            return _hp;
        }
    }

    public float mpMax;
    private float _mp;
    private float mp
    {
        get
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }
            _hp = value;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_mp / mpMax);
            }
        }
        get
        {
            return _mp;
        }
    }
}
