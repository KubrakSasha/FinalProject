using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMain : MonoBehaviour
{
    //public static event Action OnEnemyDied;

    public PlayerHealthBar healthBar;
    public PlayerExpirienceBar expirienceBar;
    public HealthSystem healthSystem;
    public LevelSystem levelSystem;
    
    void Start()
    {
        healthSystem = new HealthSystem(100);
        levelSystem = new LevelSystem();
        healthBar.Setup(healthSystem);
        healthSystem.OnDead += HealthSystem_OnDead;
        expirienceBar.Setup(levelSystem);
        EnemyMain.OnEnemyDied += EnemyMain_OnEnemyDied;
    }

    private void EnemyMain_OnEnemyDied()
    {
        levelSystem.AddExpirience(3);
        Debug.Log(levelSystem.GetLevel());
        Debug.Log(levelSystem.GetExpirience());

    }

    private void HealthSystem_OnDead()
    {        
        GameManager.Instance.UpdateGameStates(GameStates.Dead);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);
        if (enemy != null)
        {
            healthSystem.ApplyDamgage(enemy.GetDamage());
        }
    }    
}
