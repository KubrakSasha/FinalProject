using System;

public class HealthSystem
{
    public event Action OnHealthChanged;
    public event Action OnDead;
    public event Action OnDamaged;
    public event Action OnHealed;

    private int _health;
    private int _maxHealth;

    public HealthSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void ApplyDamgage(int amount) // или TakeDamage
    {
        _health -= amount;
        if (_health <= 0)
        {
            _health = 0;
        }
        OnHealthChanged?.Invoke();
        OnDamaged?.Invoke();
        if (_health <= 0)
        {
            Die();            
        }
    }
    public void Die() 
    {
        OnDead?.Invoke();
    }
    public void ApplyHeal(int amount) // или TakeDamage
    {
        _health += amount;
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        OnHealthChanged?.Invoke();
        OnHealed?.Invoke();        
    }
    public float GetHealthPercent()
    {
        return (float)_health / _maxHealth;
    }
}

