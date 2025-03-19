using UnityEngine;

public class EffectManager : Spawner<EffectCtrl>
{
    public override EffectCtrl GetPrefab(int prefab)
    {
        string name = $"Effect_{prefab}";
        return _prefabs.Find(_prefab => _prefab.name == name);
    }
}
