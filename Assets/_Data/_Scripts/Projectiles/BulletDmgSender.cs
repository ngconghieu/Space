using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDmgSender : DmgSender
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out DmgReceiver receiver)) return;
        receiver.ReceiveDamage(damage);
    }

    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<Collider2D>() as CapsuleCollider2D;
        if (_collider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.isTrigger = true;
            capsuleCollider.size = new Vector2(.12f, .25f);
            capsuleCollider.offset = new Vector2(0, -.05f);
        }
        Debug.Log("LoadCollider", gameObject);
    }

}