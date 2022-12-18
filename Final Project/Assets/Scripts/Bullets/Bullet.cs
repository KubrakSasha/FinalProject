using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsPoisonBullet = false;
    public bool IsExplosionBullet = true;
    public bool IsFireBullet = false;
    private void Update()
    {
            Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);
        if (enemy != null)
        {
            enemy._healthSystem.ApplyDamgage(PlayerMain.Instance.Stats.Damage);
            if (IsExplosionBullet == true)
            {
                Debug.Log("ExplosionBullet");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(enemy.transform.position, 10);
                foreach (Collider2D enemy2 in enemies) 
                {
                    enemy2.TryGetComponent<EnemyMain>(out EnemyMain enemyMain);
                    if (enemyMain != null) 
                    {
                        enemyMain._healthSystem.ApplyDamgage(PlayerMain.Instance.Stats.Damage * 2);
                        Instantiate(AssetManager.Instance.ExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
                    }
                }
            }
            //SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
            Destroy(gameObject);
        }

    }
     
}
