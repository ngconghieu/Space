using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ObstacleDmgReceiver : DmgReceiver
{
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Hurt()
    {
    }

    protected override void LoadCollider()
    {
        if(_collider != null) return;
        _collider = GetComponent<CircleCollider2D>();
        if(_collider is CircleCollider2D circleCollider)
        {
            circleCollider.isTrigger = false;
            circleCollider.radius = 1;
        }
    }
}
