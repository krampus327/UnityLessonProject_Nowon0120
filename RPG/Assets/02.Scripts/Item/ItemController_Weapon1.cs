using UnityEngine;
public class ItemController_Weapon1 : ItemController_Equipment
{
    public GameObject equipmentPrefab;

    public override void Use()
    {
        base.Use();
        Player.instance.EquipWeapon1(equipmentPrefab);
    }
}