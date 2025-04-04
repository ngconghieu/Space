using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnBullet _despawnBullet;
    [SerializeField] private BulletDmgSender _bulletDmgSender;
    [SerializeField] private EffectManager _effectManager;

    public DespawnBullet DespawnBullet => _despawnBullet;
    public BulletDmgSender BulletDmgSender => _bulletDmgSender;
    public EffectManager EffectManager => _effectManager;

    private void Start()
    {
        _despawnBullet.Initialize(this);
        _bulletDmgSender.Initialize(this);
        _effectManager = ServiceLocator.Get<EffectManager>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnBullet();
        LoadBulletDmgSender();
    }

    private void LoadDespawnBullet()
    {
        if (_despawnBullet != null) return;
        _despawnBullet = GetComponentInChildren<DespawnBullet>();
        Debug.Log("LoadDespawnBullet", gameObject);
    }

    private void LoadBulletDmgSender()
    {
        if (_bulletDmgSender != null) return;
        _bulletDmgSender = GetComponentInChildren<BulletDmgSender>();
        Debug.Log("LoadBulletDmgSender", gameObject);
    }
}
