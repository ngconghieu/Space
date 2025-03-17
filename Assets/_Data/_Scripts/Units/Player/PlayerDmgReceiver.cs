using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDmgReceiver : DmgReceiver
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _collider;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigibody();
        LoadCollider();
    }

    private void LoadRigibody()
    {
        if (_rb != null) return;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        Debug.Log("LoadRigibody", gameObject);
    }

    private void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.isTrigger = true;
        _collider.size = new Vector2(.5f, 1);
        _collider.offset = new Vector2(0, -0.02f);
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

}