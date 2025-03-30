public class EffectManager : Spawner<EffectCtrl>
{
    protected override void RegisterServices()
    {
        ServiceLocator.Register<EffectManager>(this);
    }

    protected override void Initialize(EffectCtrl prefab)
    {
        prefab.DespawnEffect.OnDespawn += Despawn;
    }
}
