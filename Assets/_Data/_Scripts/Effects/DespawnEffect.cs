using System;
using UnityEngine;

public class DespawnEffect : Despawner<EffectCtrl>
{
    [SerializeField] private float _despawnTime = 2f;
    private float _timer = 0f;

    private void FixedUpdate()
    {
        HandleDespawn();
    }

    public override void Initialize(EffectCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

    private void HandleDespawn()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _despawnTime)
            Despawn();
    }

    private void OnEnable()
    {
        _timer = 0;
    }
}
