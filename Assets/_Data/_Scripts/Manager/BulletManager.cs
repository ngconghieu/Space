using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            InventoryManager.Instance.AddItem(PrefabName.Material_Rock_0, 100);
            InventoryManager.Instance.RemoveItem(PrefabName.Material_Rock_0, 90);
        }
    }
}
