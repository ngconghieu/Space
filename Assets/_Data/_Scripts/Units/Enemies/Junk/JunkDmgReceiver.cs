using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class JunkDmgReceiver : DmgReceiver
{
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
