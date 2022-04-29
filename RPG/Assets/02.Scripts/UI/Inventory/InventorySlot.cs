using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isItemExist;
    public int id;
    public string itemName;
    public string description;
    public int num;
    public Sprite icon;

    [SerializeField] private Image _image;

    public void SetUp(Item item, int itemnum)
    {
        itemName = item.name;
        description = item.description;
        num = itemnum;
        icon = item.icon;

        _image.sprite = icon;
    }
}
