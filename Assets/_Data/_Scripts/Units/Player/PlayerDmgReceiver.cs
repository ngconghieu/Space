using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerDmgReceiver : DmgReceiver
{
    [SerializeField] private float _delayHurt = 0.3f;
    [SerializeField] private float _invincibleTime = 1f;
    private PlayerShipCtrl ctrl;

    private IEnumerator HandleHit()
    {
        ctrl.ShipMovement.CanMove = false;
        col.enabled = false;
        yield return new WaitForSeconds(_delayHurt);
        ctrl.Rb.linearVelocity = Vector2.zero;
        ctrl.ShipMovement.CanMove = true;
        yield return new WaitForSeconds(_invincibleTime);
        col.enabled = true;
    }

    public override void Die()
    {
        
    }

    public override void Hurt()
    {
        StartCoroutine(HandleHit());
    }

    protected override void LoadCollider()
    {
        if (col != null) return;
        col = GetComponent<CapsuleCollider2D>();
        if (col is CapsuleCollider2D capsuleCollider2D)
        {
            capsuleCollider2D.isTrigger = true;
            capsuleCollider2D.size = new Vector2(.5f, 1);
            capsuleCollider2D.offset = new Vector2(0, -0.02f);
        }
        Debug.Log("LoadCollider", gameObject);
    }

    public void Initialize(PlayerShipCtrl playerShipCtrl)
    {
        ctrl = playerShipCtrl;
    }

}