using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Rockat : Tower
{
    public GameObject RocketPrefab;
    public Transform firePoint;
    public int damage;
    public float reloadTime;
    private float reloadTimer;
    public override void Update()
    {
        base.Update();
        if (reloadTimer < 0)
        {
            if (Target != null)
            {
                Attack();
                reloadTimer = reloadTime;
            }
        }
        else
            reloadTimer -= Time.deltaTime;
    }
    
}
