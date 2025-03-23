using System;
using System.Collections;
using UnityEngine;

public class ShipAttack : GameMonoBehaviour
{
    [SerializeField] private BulletCtrl _bulletCtrl;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private bool _isFire = false;
    [SerializeField] private float _fireRate = 0.1f;
    private PlayerShipCtrl _playerShipCtrl;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new(_fireRate);
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        HandleFire();
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

    public void Initialize(PlayerShipCtrl ctrl)
    {
        this._playerShipCtrl = ctrl;
    }
    #endregion


    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (_isFire)
                Fire();
            yield return _wait;
        }
    }

    private void Fire()
    {
        if (_bulletCtrl == null)
            _bulletCtrl = _playerShipCtrl.BulletManager.GetPrefab(PrefabName.BulletDefault);
        _playerShipCtrl.BulletManager.Spawn(
            _bulletCtrl,
            _firePoint.position,
            transform.parent.rotation
        );
    }

    private void HandleFire()
    {
        _isFire = InputManager.Instance.isLeftClick;
    }

    //public void SetFireRate(float fireRate)
    //{
    //    if(Mathf.Approximately(_fireRate, fireRate)) return;
    //    _fireRate = fireRate;
    //    _wait = new WaitForSeconds(_fireRate);
    //}
}
