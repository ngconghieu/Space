using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] protected Collider2D col;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 10;

    public int Health => health;

    protected virtual void OnEnable()
    {
        SetParameters();
    }

    protected virtual void OnDisable()
    {
        col.enabled = false;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    protected abstract void LoadCollider();
    #endregion

    protected virtual void SetParameters()
    {
        health = maxHealth;
        col.enabled = true;
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
        Hurt();
    }

    public void Heal(int health)
    {
        this.health += health;
        if (this.health > maxHealth)
            this.health = maxHealth;
    }

    public abstract void Die();

    public abstract void Hurt();
}
