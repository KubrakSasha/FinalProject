using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMain : Singleton<PlayerMain>
{
    //public static event Action OnEnemyDied;

    public PlayerHealthBar healthBar;
    public PlayerExpirienceBar expirienceBar;
    public HealthSystem healthSystem;
    public LevelSystem levelSystem;
    public GameObject ExplosionPrefab;

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
        //Debug.Log(levelSystem.GetLevel());
        //Debug.Log(levelSystem.GetExpirience());
    }

    private void HealthSystem_OnDead()
    {
        GameManager.Instance.UpdateGameStates(GameStates.Dead);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);// Как сравнивать типы наследников
        if (enemy != null)
        {
            healthSystem.ApplyDamgage(enemy.GetDamage());
            if (enemy.IsExplosive)
            {
                GameObject explosion1 = Instantiate(ExplosionPrefab, enemy.transform.position, Quaternion.identity) as GameObject;
                GameManager.Instance.CameraShake.Shake(0.5f, 0.5f);
                Destroy(enemy.gameObject);
            }
        }
        collision.collider.TryGetComponent<EnemyBullet>(out EnemyBullet enemyBullet);
        if (enemyBullet != null)
        {
            healthSystem.ApplyDamgage(enemyBullet.GetDamage());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Heal>(out Heal heal);
        if (heal != null)
        {
            healthSystem.ApplyHeal(30);
            Destroy(collision.gameObject);
        }
        

        collision.TryGetComponent<Explosion>(out Explosion explosion);
        if (explosion != null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 15);
            foreach (Collider2D enemy in enemies) 
            {
                enemy.TryGetComponent<EnemyMain>(out EnemyMain enem);
                if (enem != null)
                {
                    enem._healthSystem.ApplyDamgage(100);
                }                
            }
            GameManager.Instance.CameraShake.Shake(0.5f, 0.5f);
            GameObject explosion1 = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(collision.gameObject);
        }
    }
}
