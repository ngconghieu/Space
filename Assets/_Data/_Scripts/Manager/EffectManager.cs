using UnityEngine;

public class EffectManager : Spawner<EffectCtrl>
{
    protected override void SubcribeEvent(EffectCtrl prefab)
    {
        prefab.DespawnEffect.OnDespawn += Despawn;
    }
}
