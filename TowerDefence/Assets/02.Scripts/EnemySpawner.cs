using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SpawnElement[] spawnElements;
    [System.Serializable]
    class SpawnElement
    {
        public PoolElement PoolElement;
        public float spawnTimeGap;
        public bool done;
    }
    float[] spawnTimer;
    int[] spawnCounts;

    Transform tr;
    private void Awake()
    {
        tr = transform;
        spawnTimer = new float[spawnElements.Length];
        spawnCounts = new int[spawnElements.Length];
        for (int i = 0; i< spawnElements.Length; i++)
        {
            spawnTimer[i] = spawnElements[i].spawnTimeGap;
            ObjectPool.instance.AddPoolElement(spawnElements[i].PoolElement);
        }
    }
    private void Update()
    {
        for (int i = 0; i < spawnElements.Length; i++)
        {
            string tmpTag = spawnElements[i].PoolElement.tag;
            int num = ObjectPool.GetSpawnedObjectNumber(tmpTag);
            if (spawnCounts[i] < spawnElements[i].PoolElement.size)
            {
                if (num < spawnElements[i].PoolElement.size)
                {
                    if (spawnTimer[i] < 0)
                    {
                        Spawn(tmpTag);
                        spawnTimer[i] = spawnElements[i].spawnTimeGap;
                        spawnCounts[i]++;
                    }
                    else
                        spawnTimer[i] -= Time.deltaTime;
                }
                else
                    spawnElements[i].done = true;
            }
            
        }
    }
    private void Spawn(string tag)
    {
        ObjectPool.SpawnFromPool(tag, tr.position);
    }
}
