using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public static PlayerUI instance;

    [SerializeField] private Slider hpBar;

    public void SetHPBar(float value) =>
        hpBar.value = value;

    private void Awake()
    {
        instance = this;
    }
}
