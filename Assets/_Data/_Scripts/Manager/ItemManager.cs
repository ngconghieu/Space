public class ItemManager : Spawner<ItemCtrl>
{
    protected override void RegisterServices()
    {
        ServiceLocator.Register<ItemManager>(this);

    }

    protected override void SubscribeEvent(ItemCtrl prefab)
    {
        prefab.DespawnItem.OnDespawn += Despawn;
    }
}

public enum ItemType
{
    None = 0,
    Equipment = 1,
    Consumable = 2,
    Material = 3,
}

public enum ItemName
{
    None = 0,
    CopperOre = 1,
    GoldOre = 2,
}