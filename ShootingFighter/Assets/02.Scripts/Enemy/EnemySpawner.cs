using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ư�� �ð� �������� enemy �� �����ϴ� ��ũ��Ʈ
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay;
    float elapsedTime;

    Transform tr;
    private void Awake()
    {
        tr = gameObject.transform;
    }
    void Update()
    {
        if(elapsedTime > spawnDelay)
        {
            Instantiate(enemyPrefab, tr);
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }
}
