using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ObstacleDmgReceiver : DmgReceiver
{
    private ObstacleCtrl ctrl;

    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CircleCollider2D>();
        if (_collider is CircleCollider2D circleCollider)
        {
            circleCollider.isTrigger = true;
            circleCollider.radius = .8f;
        }
    }

    public override void Die()
    {
        _collider.enabled = false;
        SpawnEffect();
        ctrl.DespawnObstacle.Despawn();
    }

    private void SpawnEffect()
    {
        EffectCtrl effectCtrl = ctrl.EffectManager.GetPrefab("Effect_1");
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
