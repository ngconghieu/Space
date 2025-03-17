using System;
using UnityEngine;

public class ShipAttack : GameMonoBehaviour
{
    [SerializeField] private BulletCtrl _bulletCtrl;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private bool _isFire = false;
    [SerializeField] private int _bulletIndex = 0;
    [SerializeField] private float _fireRate = 0.1f;

    private void Start()
    {
        LoadParameters();
        InvokeRepeating(nameof(Fire), _fireRate, 1);
    }


    private void Update()
    {
        HandleFire();
    }

    private void LoadParameters()
    {
        _bulletCtrl = BulletManager.Instance.GetPrefab(_bulletIndex);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFirePoint();
    }

    private void LoadFirePoint()
    {
        if (_firePoint != null) return;
        _firePoint = transform.Find("FirePoint");
        if (_firePoint == null)
        {
            _firePoint = new GameObject("FirePoint").transform;
            _firePoint.transform.SetParent(transform);
            _firePoint.transform.localPosition = new Vector2(0, .55f);
        }
        Debug.Log("LoadFirePoint", gameObject);
    }
    #endregion

    private void Fire()
    {
        if (!_isFire) return;
        BulletManager.Instance.Spawn(
            _bulletCtrl, 
            _firePoint.position, 
            transform.parent.rotation
        );
    }

    private void HandleFire()
    {
        _isFire = InputManager.Instance.isLeftClick;
    }
}
