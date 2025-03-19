using System;
using UnityEngine;

public class DespawnEffect : Despawner<EffectCtrl>
{
    [SerializeField] private EffectCtrl _effectCtrl;
    [SerializeField] private float _despawnTime = 2f;
    private float _timer = 0f;

    private void FixedUpdate()
    {
        HandleDespawn();
    }

    private void HandleDespawn()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _despawnTime)
            Despawn(_effectCtrl);
    }

    protected override void LoadCtrl()
    {
        if (_effectCtrl != null) return;
        _effectCtrl = GetComponentInParent<EffectCtrl>();
        Debug.Log("LoadEffectCtrl", gameObject);
    }

    protected override void OnEnable()
    {
        _timer = 0f;
    }
}
