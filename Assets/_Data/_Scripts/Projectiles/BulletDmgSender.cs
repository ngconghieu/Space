using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDmgSender : DmgSender
{
    [SerializeField] private BulletCtrl _bulletCtrl;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out DmgReceiver receiver)) return;
        receiver.ReceiveDamage(damage);
        EffectCtrl effect = EffectManager.Instance.GetPrefab("Effect_0");
        Quaternion rotation = Quaternion.Euler(0, 0, transform.parent.rotation.z);
        EffectManager.Instance.Spawn(
            effect,
            transform.parent.position,
            rotation
        );
        _bulletCtrl.DespawnBullet.Despawn(_bulletCtrl);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBulletCtrl();
    }

    private void LoadBulletCtrl()
    {
        if (_bulletCtrl != null) return;
        _bulletCtrl = GetComponentInParent<BulletCtrl>();
        Debug.Log("LoadBulletCtrl", gameObject);
    }

    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<Collider2D>() as CapsuleCollider2D;
        if (_collider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.isTrigger = true;
            capsuleCollider.size = new Vector2(.15f, .3f);
        }
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion
}