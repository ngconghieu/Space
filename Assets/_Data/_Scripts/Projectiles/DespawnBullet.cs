using System;
using UnityEngine;

public class DespawnBullet : Despawner<BulletCtrl>
{
    [SerializeField] private BulletCtrl _bulletCtrl;
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
            Despawn(_bulletCtrl);
    }

    protected override void LoadCtrl()
    {
        if (_bulletCtrl != null) return;
        _bulletCtrl = GetComponentInParent<BulletCtrl>();
        Debug.Log("LoadBulletCtrl", gameObject);
    }

    
}