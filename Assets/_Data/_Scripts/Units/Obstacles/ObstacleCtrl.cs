using System;
using UnityEngine;

public class ObstacleCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnObstacle _despawnObstacle;
    public DespawnObstacle DespawnObstacle => _despawnObstacle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnObstacle();
    }

    private void LoadDespawnObstacle()
    {
        if(_despawnObstacle != null) return;
        _despawnObstacle = GetComponentInChildren<DespawnObstacle>();
        Debug.Log("LoadDespawnObstacle", gameObject);
    }
}
