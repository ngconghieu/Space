using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class DmgSender : GameMonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected int damage = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    public void SetDmg(int dmg)
    {
        damage = dmg;
    }
    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected abstract void LoadCollider();
}
