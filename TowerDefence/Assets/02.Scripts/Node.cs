using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color originColor;
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
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = originColor;
    }

    private void OnMouseDown()
    {
        if (TowerViewPresenter.Instance.isSelected)
        {
            Transform previewTransform = TowerViewPresenter.Instance.GetTowerPreviewTransform();
            ObjectPool.SpawnFromPool(previewTransform.GetComponent<TowerPreview>().towerName, previewTransform.position);
        }
    }
}
