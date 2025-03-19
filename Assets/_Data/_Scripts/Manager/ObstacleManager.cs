using UnityEngine;

public class ObstacleManager : Spawner<ObstacleCtrl>
{
    public override ObstacleCtrl GetPrefab(int prefab)
    {
        string name = $"Obstacle_{prefab}";
        return _prefabs.Find(_prefab => _prefab.name == name);
    }
}
