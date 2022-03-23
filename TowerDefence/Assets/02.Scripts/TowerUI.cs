using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;
    private void Awake()
    {
        instance = this;
    }

    public TowerInfo info;

    public Text upgradePriceText;
    public Text sellPriceText;
    private void OnDisable()
    {
        info = null;
        upgradePriceText = "";
        sellPriceText = "";
    }
    public void OnUpgradeButton()
    {
        int nextLevel = Node.towerInfo.level + 1;
        if(TowerAssets.Instance.TryGetTowerName(Node.towerInfo.type, nextLevel, out))
        {

        }
    }
    public void OnSellButton()
    {

    }
}
