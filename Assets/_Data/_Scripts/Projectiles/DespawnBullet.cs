using System;
using System.Collections;
using UnityEngine;

public class DespawnBullet : Despawner<BulletCtrl>
{
    [SerializeField] private float _timeToDespawn = 6;

    public override void Initialize(BulletCtrl ctrl)
    {
        this.ctrl = ctrl;
        StartCoroutine(HandleDespawn());
    }

    private IEnumerator HandleDespawn()
    {
        yield return new WaitForSeconds(_timeToDespawn);
        Despawn();
    }
}
