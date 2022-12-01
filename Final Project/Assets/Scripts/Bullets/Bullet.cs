using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent<EnemyMain>(out EnemyMain enemy);
        if (enemy != null)
        {
            enemy.healthSystem.ApplyDamgage(100);
            Destroy(gameObject);
        }
    }
}
