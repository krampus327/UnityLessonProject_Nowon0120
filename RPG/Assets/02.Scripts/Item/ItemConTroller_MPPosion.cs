public class ItemConTroller_MPPosion : ItemController
{
    public override void Use()
    {
        base.Use();
        Player.instance.mp += 100;
    }
}