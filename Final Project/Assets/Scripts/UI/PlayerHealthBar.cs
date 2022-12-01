using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Transform bar;
    HealthSystem healthSystem;


    private void Start()
    {
       
    }
    public void Setup(HealthSystem healthSystem) 
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged()
    {
        bar.localScale = new Vector2(healthSystem.GetHealthPercent(), 1);
    }
}
