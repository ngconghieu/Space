using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ObstacleDmgReceiver : DmgReceiver
{
    private ObstacleCtrl ctrl;

    protected override void LoadCollider()
    {
        if (col != null) return;
        col = GetComponent<CircleCollider2D>();
        if (col is CircleCollider2D circleCollider)
        {
            circleCollider.isTrigger = true;
            circleCollider.radius = .8f;
        }
    }

    public override void Die()
    {
        SpawnEffect();
        ctrl.DespawnObstacle.Despawn();
    }

    private void SpawnEffect()
    {
        EffectCtrl effectCtrl = ctrl.EffectManager.GetPrefab(PrefabName.Smoke_ObstacleDetroy);
        ctrl.EffectManager.Spawn(effectCtrl, ctrl.transform.position, Quaternion.identity);
    }

    public override void Hurt()
    {
    }

    public void Initialize(ObstacleCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

}
