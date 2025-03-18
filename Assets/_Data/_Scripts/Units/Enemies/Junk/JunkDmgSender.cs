using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class JunkDmgSender : DmgSender
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out DmgReceiver receiver)) return;
        receiver.ReceiveDamage(damage);
    }

    protected override void SetValues()
    {
        base.SetValues();
        SetDmg(2);
    }
    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CircleCollider2D>();
        if(_collider is CircleCollider2D circleCollider)
        {
            circleCollider.isTrigger = true;
            circleCollider.radius = 1;

        }
        Debug.Log("LoadCollider", gameObject);
    }

    
}