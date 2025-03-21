using System;
using UnityEngine;

public class DespawnBullet : Despawner<BulletCtrl>
{
    [SerializeField] private float _timeToDespawn = 6;
    private float _timer;


    private void OnEnable()
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
            Despawn();
    }

    public override void Initialize(BulletCtrl ctrl)
    {
        this.ctrl = ctrl;
    }
}
