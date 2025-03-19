using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerDmgReceiver : DmgReceiver
{
    [SerializeField] private float _force = 10;
    public override void Die()
    {
        
    }

    public override void Hurt()
    {
        rb.AddForce(_force * Vector2.down);
    }

    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CapsuleCollider2D>();
        if (_collider is CapsuleCollider2D capsuleCollider2D)
        {
            capsuleCollider2D.isTrigger = true;
            capsuleCollider2D.size = new Vector2(.5f, 1);
            capsuleCollider2D.offset = new Vector2(0, -0.02f);
        }
        Debug.Log("LoadCollider", gameObject);
    }
}