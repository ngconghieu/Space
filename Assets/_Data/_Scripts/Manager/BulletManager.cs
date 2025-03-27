public class BulletManager : Spawner<BulletCtrl>
{
    protected override void RegisterServices()
    {
        ServiceLocator.Register<BulletManager>(this);
    }

    protected override void SubscribeEvent(BulletCtrl prefab)
    {
        prefab.DespawnBullet.OnDespawn += Despawn;
    }
}
