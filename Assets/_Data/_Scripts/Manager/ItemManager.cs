public class ItemManager : Spawner<ItemCtrl>
{
    protected override void SubcribeEvent(ItemCtrl prefab)
    {
        prefab.DespawnItem.OnDespawn += Despawn;
    }
}