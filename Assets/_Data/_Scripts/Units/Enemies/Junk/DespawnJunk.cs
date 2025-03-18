using System;
using UnityEngine;
public class DespawnJunk : Despawner<ObstacleCtrl>
{
    [SerializeField] private JunkDmgReceiver dmgReceiver;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadJunkDmgReceiver();
    }

    private void LoadJunkDmgReceiver()
    {
        if(dmgReceiver != null) return;
        dmgReceiver = transform.parent.GetComponentInChildren<JunkDmgReceiver>();
        Debug.Log("LoadJunkDmgReceiver", gameObject);
    }
    #endregion

    private void OnEnable()
    {
        dmgReceiver.OnDeath += OnDeath;
    }

    private void OnDeath()
    {

    }
}