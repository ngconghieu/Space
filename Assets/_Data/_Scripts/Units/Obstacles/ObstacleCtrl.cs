using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleCtrl : GameMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private DespawnObstacle _despawnObstacle;
    [SerializeField] private ObstacleDmgReceiver _obstacleDmgReceiver;
    [SerializeField] private EffectManager _effectManager;

    public DespawnObstacle DespawnObstacle => _despawnObstacle;
    public ObstacleDmgReceiver ObstacleDmgReceiver => _obstacleDmgReceiver;
    public EffectManager EffectManager => _effectManager;

    private void Start()
    {
        _despawnObstacle.Initialize(this);
        _obstacleDmgReceiver.Initialize(this);
        _effectManager = ServiceLocator.Get<EffectManager>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigibody();
        LoadDespawnObstacle();
        LoadObstacleDmgReceiver();
    }

    private void LoadObstacleDmgReceiver()
    {
        if (_obstacleDmgReceiver != null) return;
        _obstacleDmgReceiver = GetComponentInChildren<ObstacleDmgReceiver>();
        Debug.Log("LoadObstacleDmgReceiver", gameObject);
    }

    private void LoadDespawnObstacle()
    {
        if(_despawnObstacle != null) return;
        _despawnObstacle = GetComponentInChildren<DespawnObstacle>();
        Debug.Log("LoadDespawnObstacle", gameObject);
    }

    private void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.angularDamping = 0;
        rb.mass = 1;
        Debug.Log("LoadRigibody", gameObject);
    }
}
