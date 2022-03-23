using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool isTowerHere
    {
        get 
        {
            return towerInfo != null;
        }
    }
    public TowerInfo towerInfo;

    private Color originColor;
    public Color buildAvailableColor;
    public Color buildNotAvailableColor;

    Renderer rend;
    BoxCollider col;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<BoxCollider>();
        originColor = rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = buildAvailableColor;
        if (TowerViewPresenter.Instance.isSelected)
        {
            Transform previewTransform = TowerViewPresenter.Instance.GetTowerPreviewTransform();
            previewTransform.gameObject.SetActive(true);
            previewTransform.position = transform.position + new Vector3(0, col.size.y / 2);
            if (isTowerHere)
                rend.material.color = buildNotAvailableColor;
            else
                rend.material.color = buildAvailableColor;
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = originColor;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            if(isTowerHere && TowerViewPresenter.Instance.isSelected == false)
            {
                TowerUI.instance.transform.position = transform.position + Vector3.up * 2;
                TowerUI.instance.upgradePriceText.text = towerInfo.price.ToString();
                TowerUI.instance.sellPriceText.text = (towerInfo.price * 0.8).ToString();
                TowerUI.instance.node = this;
                TowerUI.instance.gameObject.SetActive(true);
            }
            else if
                (isTowerHere == false && TowerViewPresenter.Instance.isSelected)
            {
                Transform previewTransform = TowerViewPresenter.Instance.GetTowerPreviewTransform();
                string towerName = previewTransform.GetComponent<TowerPreview>().towerName;
                ObjectPool.SpawnFromPool(previewTransform.GetComponent<TowerPreview>().towerName, previewTransform.position);

                if(TowerAssets.Instance.TryGetTowerInfoByName(towerName, out TowerInfo tmptowerInfo))
                {
                    // todo -> spend money : towerInfo.price
                    towerInfo = tmptowerInfo;
                }
                previewTransform.gameObject.SetActive(false);
                TowerViewPresenter.Instance.SetTowerHandler(null);
            }
        }
        else
        {
            TowerViewPresenter.Instance.SetTowerHandler(null);
        }
        
    }
}
