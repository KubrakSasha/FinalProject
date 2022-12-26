using UnityEngine;

public class Bullet : MonoBehaviour
{       
    private void Update()
    {
       Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);
        if (enemy != null)
        {
            enemy.HealthSystem.ApplyDamgage(PlayerMain.Instance.Stats.Damage);

            if (PlayerMain.Instance.Stats.IsBulletExplosive == true)
            {
                Debug.Log("ExplosionBullet");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(enemy.transform.position, 2);
                foreach (Collider2D enemy2 in enemies) 
                {
                    enemy2.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
                    if (enemyMain != null) 
                    {
                        enemyMain.HealthSystem.ApplyDamgage(PlayerMain.Instance.Stats.Damage/2);
                        Instantiate(AssetManager.Instance.SmallExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
                    }
                }
            }
            if (PlayerMain.Instance.Stats.IsBulletPoison == true) 
            {
                Debug.Log("PoisonBullet");
                StartCoroutine(enemy.HealthSystem.ApplyPoisoDamage(PlayerMain.Instance.Stats.Damage/2)); 
            }           
            Destroy(gameObject);
        }

    }
    
     
}
