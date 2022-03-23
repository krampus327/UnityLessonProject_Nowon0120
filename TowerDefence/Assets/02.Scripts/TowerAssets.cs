using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerAssets : MonoBehaviour
{
    public static TowerAssets _instance;
    public static TowerAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<TowerAssets>("TowerAssets"));
                _instance.RegisterAllTowerToObjectPool();
            }
            return _instance;
        }
    }
    public List<GameObject> towers = new List<GameObject> ();

    public void RegisterAllTowerToObjectPool()
    {
        foreach(GameObject tower in towers)
        {
            ObjectPool.instance.AddPoolElement(new PoolElement
            {
                prefab = tower,
                size = 20,
                tag = tower.name
            });
        }
    }
    public bool TryGetTowerName(TowerType type, int level)
    {
        Tower.Find(x => x.GetComponent<Tower>().info.type == type &&, Matrix4x4.GetComponent<Tower)
    }
    public bool TryGetTowerInfoByName(string towerName, out TowerInfo towerInfo)
    {
        towerInfo = towers.Find(x => x.name == towerName).GetComponent<TowerInfo>(); towers.Find(x => x.name == towerName).GetComponent<TowerInfo>();
        return towerInfo != null ? true : false;
    }
}
