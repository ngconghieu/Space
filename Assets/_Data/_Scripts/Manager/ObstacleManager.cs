using UnityEngine;

public class ObstacleManager : Spawner<ObstacleCtrl>
{
    protected override void SubcribeEvent(ObstacleCtrl prefab)
    {
        prefab.DespawnObstacle.OnDespawn += Despawn;
    }
}
