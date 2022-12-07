using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerHealthBar healthBar;
    public PlayerExpirienceBar expirienceBar;
    public HealthSystem healthSystem;
    public LevelSystem levelSystem;
    public GameObject ExplosionPrefab;

    private int _maxHealth = 100;
    private int _damage = 60;
    public int MaxHealth => _maxHealth;
    public int Damage => _damage;
    void Start()
    {
        healthSystem = new HealthSystem(_maxHealth);
        healthBar.Setup(healthSystem);
        healthSystem.OnDead += HealthSystem_OnDead;

        levelSystem = new LevelSystem();
        expirienceBar.Setup(levelSystem);
        EnemyMain.OnEnemyDied += EnemyMain_OnEnemyDied;
    }
    private void EnemyMain_OnEnemyDied()
    {
        levelSystem.AddExpirience(3);        
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
            healthSystem.ApplyDamgage(enemy.Damage);
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
            healthSystem.ApplyDamgage(enemyBullet.Damage);
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
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
