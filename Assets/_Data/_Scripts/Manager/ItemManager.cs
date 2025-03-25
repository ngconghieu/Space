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
