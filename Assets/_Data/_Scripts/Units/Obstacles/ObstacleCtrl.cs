using System;
using UnityEngine;

public class ObstacleCtrl : GameMonoBehaviour
{
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
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnObstacle();
        LoadObstacleDmgReceiver();
        LoadEffectManager();
    }

    private void LoadEffectManager()
    {
        if (_effectManager != null) return;
        _effectManager = FindAnyObjectByType<EffectManager>();
        Debug.Log("LoadEffectManager", gameObject);
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
}
