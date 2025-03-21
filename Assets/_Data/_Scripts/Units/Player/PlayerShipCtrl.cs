using System;
using UnityEngine;

public class PlayerShipCtrl : GameMonoBehaviour
{
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private ShipAttack _shipAttack;

    public BulletManager BulletManager => _bulletManager;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _shipAttack.Initialize(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBulletManager();
        LoadShipAttack();
    }

    private void LoadShipAttack()
    {
        if (_shipAttack != null) return;
        _shipAttack = GetComponentInChildren<ShipAttack>();
        Debug.Log("LoadShipAttack", gameObject);
    }

    private void LoadBulletManager()
    {
        if (_bulletManager != null) return;
        _bulletManager = FindAnyObjectByType<BulletManager>();
        Debug.Log("LoadBulletManager", gameObject);
    }

}
