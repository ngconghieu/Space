using UnityEngine;

public class BulletManager : Spawner<BulletCtrl>
{
    public override BulletCtrl GetPrefab(int prefab)
    {
        string name = $"Bullet_{prefab}";
        return _prefabs.Find(_prefab => _prefab.name == name);
    }
}
