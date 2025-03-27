using System;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class DespawnItem : Despawner<ItemCtrl>
{
    [SerializeField] private CircleCollider2D _collider;
    public override void Initialize(ItemCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
        _collider.radius = 0.5f;
        _collider.offset = new Vector2(0, 0);
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<PlayerDmgReceiver>(out _)) return;
        Const name = (Const)Enum.Parse(typeof(Const), ctrl.name);
        InventoryManager.Instance.AddItem(name, 1);
        Despawn();
    }
}