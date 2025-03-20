using System;
using UnityEngine;

public class DespawnBullet : Despawner<BulletCtrl>
{
    [SerializeField] private BulletCtrl _bullet;
    [SerializeField] private float _timeToDespawn = 6;
    private float _timer;

    protected override void OnEnable()
    {
        _timer = 0;
    }

    private void FixedUpdate()
    {
        HandleDespawn();
    }

    private void HandleDespawn()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _timeToDespawn)
            Despawn(_bullet);
    }
}
