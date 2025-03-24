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
        ctrl.DespawnObstacle.Despawn();
    }

    public override void Hurt()
    {
    }

    public void Initialize(ObstacleCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

}
