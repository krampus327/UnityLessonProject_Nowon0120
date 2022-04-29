using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsView : MonoBehaviour
{
    public Transform content;
    public int totalSlotNumber = 12;
    public GameObject slotPrefab;
    private List<InventorySlot> slots = new List<InventorySlot>();
    
    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        for(int i = 0; i < totalSlotNumber; i++)
        {
            slots.Add(Instantiate(slotPrefab, content).GetComponent<InventorySlot>());
        }
    }

    public int AddItem(Item item, int itemNum)
    {
        bool remain = itemNum;
        InventorySlot tmpSlot = slots.Find(x => x.itemName == item.name);
        // ������ �������� �̹� �����ϴ���
        if (tmpSlot != null)
        {
            // �����Ϸ��� ���� + ���� ������ ������ �ִ� ���� �����̸� �ش罽�� ���� �߰�
            if(itemNum + tmpSlot.num <= item.numMax)
            {
                tmpSlot.num += itemNum;
                remain = 0;
            }
            else
            {
                int remain = tmpSlot.num + itemNum - item.numMax; // ���� ���Կ� �߰��� ����
                int tmp = itemNum - remain; // ���� ���Կ� �߰��� ����

                tmpSlot.num += tmp;
                isAdded = true;
            }
        }
        // ������ �������� ������
        else
        {
            // �� ���� �˻�
            tmpSlot = slots.Find(x => x.isItemExist == false) || ((x.itemName == item.name) && (x.num < item.numMax));
            // �� ���� ������
            if(tmpSlot != null)
            {
                AddItem(item, itemNum);
            }
        }
        return remain;
    }
}
