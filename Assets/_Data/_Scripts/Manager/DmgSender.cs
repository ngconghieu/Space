using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class DmgSender : GameMonoBehaviour
{
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private int _damage = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if(_collider != null) return;
        _collider = GetComponent<CapsuleCollider2D>();
        Debug.Log("LoadCollider", gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out DmgReceiver receiver)) return;
        receiver.ReceiveDamage(_damage);
    }
}
