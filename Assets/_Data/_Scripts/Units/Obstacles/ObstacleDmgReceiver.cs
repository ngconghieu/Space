using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ObstacleDmgReceiver : DmgReceiver
{
    private ObstacleCtrl _obstacleCtrl;

    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CircleCollider2D>();
        if (_collider is CircleCollider2D circleCollider)
        {
            circleCollider.isTrigger = false;
            circleCollider.radius = .8f;
        }
    }

    public override void Die()
    {
        _obstacleCtrl.DespawnObstacle.Despawn();
    }

    public override void Hurt()
    {
    }

    public void Initialize(ObstacleCtrl obstacleCtrl)
    {
        _obstacleCtrl = obstacleCtrl;
    }
}
