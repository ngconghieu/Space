using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShipCtrl : GameMonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private ShipAttack _shipAttack;
    [SerializeField] private PlayerDmgReceiver _playerDmgReceiver;
    [SerializeField] private ShipMovement _shipMovement;

    public Rigidbody2D Rb => _rb;
    public BulletManager BulletManager => _bulletManager;
    public ShipMovement ShipMovement => _shipMovement;

    private void Start()
    {
        _shipAttack.Initialize(this);
        _playerDmgReceiver.Initialize(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigibody();
        LoadBulletManager();
        LoadShipAttack();
        LoadPlayerDmgReceiver();
        LoadShipMovement();
    }

    private void LoadShipMovement()
    {
        if (_shipMovement != null) return;
        _shipMovement = GetComponentInChildren<ShipMovement>();
        Debug.Log("LoadShipMovement", gameObject);
    }

    private void LoadPlayerDmgReceiver()
    {
        if (_playerDmgReceiver != null) return;
        _playerDmgReceiver = GetComponentInChildren<PlayerDmgReceiver>();
        Debug.Log("LoadPlayerDmgReceiver", gameObject);
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

    private void LoadRigibody()
    {
        if (_rb != null) return;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.angularDamping = 0;
        _rb.mass = 1;
        Debug.Log("LoadRigibody", gameObject);
    }
}
