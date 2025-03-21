using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletManager : Spawner<BulletCtrl>
{
    protected override void SubcribeEvent(BulletCtrl prefab)
    {
        prefab.DespawnBullet.OnDespawn += Despawn;
    }
}
