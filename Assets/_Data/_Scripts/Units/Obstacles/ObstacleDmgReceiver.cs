using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ObstacleDmgReceiver : DmgReceiver
{
    [SerializeField] private ObstacleCtrl _obstacleCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadObstacleCtrl();
    }

    private void LoadObstacleCtrl()
    {
        if (_obstacleCtrl != null) return;
        _obstacleCtrl = GetComponentInParent<ObstacleCtrl>();
        Debug.Log("LoadObstacleCtrl", gameObject);
    }

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
        _obstacleCtrl.DespawnObstacle.Despawn(_obstacleCtrl);
    }

    public override void Hurt()
    {
    }

}
