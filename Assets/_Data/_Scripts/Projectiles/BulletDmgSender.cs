using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDmgSender : DmgSender
{
    [SerializeField] private BulletCtrl _bulletCtrl;
    private EffectCtrl _effect;

    public void Initialize(BulletCtrl bulletCtrl)
    {
        _bulletCtrl = bulletCtrl;
    }

    private void Start()
    {
        _effect = _bulletCtrl.EffectManager.GetPrefab(Const.Impact_Bullet);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out DmgReceiver receiver)) return;
        receiver.ReceiveDamage(damage);

        float parentRotationZ = transform.parent.eulerAngles.z;
        Quaternion rotation = Quaternion.Euler(0, 0, parentRotationZ + 180);
        _bulletCtrl.EffectManager.Spawn(
            _effect,
            transform.parent.position,
            rotation
        );
        _bulletCtrl.DespawnBullet.Despawn();
    }

    #region LoadComponents
    protected override void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<Collider2D>();
        if (_collider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.isTrigger = true;
            capsuleCollider.size = new Vector2(.15f, .3f);
        }
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion
}