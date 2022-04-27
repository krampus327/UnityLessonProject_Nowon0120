using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hpMax;
    private float _hp;
    public float hp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
                DropRandomItem();
                Destroy(gameObject);
            }

            _hp = value;

            if (enemyUI != null)
            {
                enemyUI.SetHPBar(_hp / hpMax);
            }
        }

        get
        {
            return _hp;
        }

    }

    [SerializeField] private Item[] dropItems;
    [SerializeField] private EnemyUI enemyUI;
    public void Hurt(float damage)
    {
        hp -= damage;
    }

    private void Awake()
    {
        hp = hpMax;
    }

    /// <summary>
    /// �����ϰ� �������� ����ϴ� �Լ�
    /// </summary>
    private void DropRandomItem()
    {
        // ��Ӿ����� ����� �ִ��� üũ
        if (dropItems == null || 
            dropItems.Length <= 0) 
            return;

        // ����� ������ 
        Item tmpItem = dropItems[Random.Range(0, dropItems.Length)];

        // ����� ������ ���
        if (tmpItem != null)
            Instantiate(ItemAssets.GetItemPrefab(tmpItem.name), transform.position, Quaternion.identity);
    }

}
