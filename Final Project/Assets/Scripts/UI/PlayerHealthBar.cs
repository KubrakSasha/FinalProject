using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Transform _bar;
    private HealthSystem _healthSystem;

    public void Setup(HealthSystem healthSystem) 
    {
        this._healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged()
    {
        _bar.localScale = new Vector2(_healthSystem.GetHealthPercent(), 1);
    }
}
