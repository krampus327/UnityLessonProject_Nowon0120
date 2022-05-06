public class ItemConTroller_HPPosion : ItemController
{
    public override void Use()
    {
        base.Use();
        Player.instance.hp += 100;
    }
}