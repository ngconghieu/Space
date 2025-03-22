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
        _collider.radius = 0.55f;
        _collider.offset = new Vector2(0, -0.1f);
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Despawn();
    }
}