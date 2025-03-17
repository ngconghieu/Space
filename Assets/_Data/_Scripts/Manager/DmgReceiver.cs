using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth = 10;

    private void OnEnable()
    {
        SetHealth();
    }

    protected void SetHealth()
    {
        _health = _maxHealth;
    }

    protected void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        SetHealth();
    }

    public void ReceiveDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public void Heal(int health)
    {
        _health += health;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

}
