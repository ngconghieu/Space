using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 10;
    public event Action OnDeath;

    public int Health => health;

    private void OnEnable()
    {
        SetParameters();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigibody();
        LoadCollider();
    }

    protected void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        Debug.Log("LoadRigibody", gameObject);
    }

    protected abstract void LoadCollider();
    #endregion

    protected void SetParameters()
    {
        health = maxHealth;
    }

    protected void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        SetParameters();
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        NotifyDeath();
        gameObject.SetActive(false);
    }

    public void Heal(int health)
    {
        this.health += health;
        if (this.health > maxHealth)
            this.health = maxHealth;
    }

    public void NotifyDeath()
    {
        OnDeath?.Invoke();
    }
}
