using System;
using System.Collections;
using UnityEngine;

public class HealthSystem
{
    public event Action OnHealthChanged;
    public event Action OnDead;
    public event Action OnDamaged;
    public event Action OnHealed;

    private float _health;
    private float _maxHealth;
    public HealthSystem(float maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void ApplyDamgage(float amount) // или TakeDamage
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
    public void ApplyHeal(float amount) // или TakeDamage
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
        return _health / _maxHealth;
    }
    public void SetMaxHealth(float coeff) 
    {
        _maxHealth*=coeff;
    }
    public IEnumerator ApplyPoisoDamage(float amount) //ХЗ работает или нет??
    {
        ApplyDamgage(amount);
        yield return new WaitForSeconds(2f);
        ApplyDamgage(amount);
        yield return new WaitForSeconds(2f);
        ApplyDamgage(amount);
        yield return new WaitForSeconds(2f);
    }    
}